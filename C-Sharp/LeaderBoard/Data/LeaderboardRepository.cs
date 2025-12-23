using LeaderBoard.Data;
using LeaderBoard.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Globalization;

namespace Leaderboard.Data
{
    public class LeaderboardRepository
    {
        private readonly IRepository _repo;

        public LeaderboardRepository(IRepository repository)
        {
            _repo = repository;
        }
        public List<ContestLeaderrBoard> GenerateContestLeaderboard(int contestId)
        {
            const string sql = @"
                WITH Ranked AS (
                    SELECT PlayerID,
                           SUM(Score) AS TotalPoints,
                           DENSE_RANK() OVER (ORDER BY SUM(Score) DESC) AS RankVal
                    FROM PlayerScore
                    WHERE GameID = @ContestId
                    GROUP BY PlayerID
                )
                SELECT PlayerID, @ContestId AS ContestID, TotalPoints, RankVal
                FROM Ranked
                ORDER BY RankVal;
            ";

            var list = new List<ContestLeaderrBoard>();

            using (var rdr = _repo.ExecuteReader(sql, new { ContestId = contestId }))
            {
                while (rdr.Read())
                {
                    list.Add(new ContestLeaderrBoard
                    {
                        PlayerID = rdr.GetInt32(0),
                        ContestID = rdr.GetInt32(1),
                        TotalPoints = rdr.GetDecimal(2),
                        Rank = rdr.GetInt32(3)
                    });
                }
            }

            return list;
        }

        public void SaveContestLeaderboard(List<ContestLeaderrBoard> rows, IDbTransaction tx)
        {
            foreach (var r in rows)
            {
                const string sql = @"
                    MERGE ContestLeaderBoard AS t
                    USING (SELECT @PlayerID AS PlayerID, @ContestID AS ContestID) s
                    ON t.PlayerID = s.PlayerID AND t.ContestID = s.ContestID
                    WHEN MATCHED THEN
                        UPDATE SET TotalPoints = @TotalPoints, Rank = @Rank
                    WHEN NOT MATCHED THEN
                        INSERT (PlayerID, ContestID, TotalPoints, Rank)
                        VALUES (@PlayerID, @ContestID, @TotalPoints, @Rank);
                ";

                _repo.ExecuteNonQuery(sql, new
                {
                    r.PlayerID,
                    r.ContestID,
                    r.TotalPoints,
                    r.Rank
                });
            }
        }
        public void UpdateGlobalLeaderboard(IDbTransaction tx)
        {
            const string sql = @"
                WITH Ranked AS (
                    SELECT PlayerID,
                           SUM(Score) AS TotalPoints,
                           DENSE_RANK() OVER (ORDER BY SUM(Score) DESC) AS RankVal
                    FROM PlayerScore
                    GROUP BY PlayerID
                )
                MERGE GlobalLeaderBoard AS t
                USING Ranked AS s
                ON t.PlayerID = s.PlayerID
                WHEN MATCHED THEN
                    UPDATE SET TotalPoints = s.TotalPoints, Rank = s.RankVal
                WHEN NOT MATCHED THEN
                    INSERT (PlayerID, TotalPoints, Rank)
                    VALUES (s.PlayerID, s.TotalPoints, s.RankVal);
            ";

            _repo.ExecuteNonQuery(sql);
        }

        public string ExportContestLeaderboard(int contestId, List<ContestLeaderrBoard> rows, string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var path = Path.Combine(
                directory,
                $"Leaderboard_{contestId}_{DateTime.UtcNow:yyyyMMddHHmmss}.csv"
            );

            using (var sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.WriteLine("Rank,PlayerID,TotalPoints");
                foreach (var r in rows)
                    sw.WriteLine($"{r.Rank},{r.PlayerID},{r.TotalPoints.ToString(CultureInfo.InvariantCulture)}");
            }

            const string logSql = @"
                INSERT INTO LeaderboardExportHistory
                (ContestID, FileLocation, ExportedAt)
                VALUES (@ContestID, @Path, @Now);
            ";

            _repo.ExecuteNonQuery(logSql, new
            {
                ContestID = contestId,
                Path = path,
                Now = DateTime.UtcNow
            });

            return path;
        }
    }
}
