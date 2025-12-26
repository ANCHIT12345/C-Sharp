using LeaderBoard.Data;
using LeaderBoard.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace Leaderboard.Data
{
    public class LeaderboardRepository
    {
        private readonly IRepository _repo;

        public LeaderboardRepository(IRepository repo)
        {
            _repo = repo;
        }

        public List<ContestLeaderrBoard> GenerateContestLeaderboard(int contestId)
        {
            const string sql = @"
                SELECT 
                    ps.PlayerID,
                    SUM(ps.Score) AS TotalPoints
                FROM PlayerScore ps
                WHERE ps.GameID = @ContestId
                GROUP BY ps.PlayerID
                ORDER BY TotalPoints DESC;
            ";

            var result = new List<ContestLeaderrBoard>();

            using (var reader = _repo.ExecuteReader(sql, new { ContestId = contestId }))
            {
                int rank = 1;
                while (reader.Read())
                {
                    result.Add(new ContestLeaderrBoard
                    {
                        PlayerID = reader.GetInt32(0),
                        ContestID = contestId,
                        TotalPoints = reader.GetDecimal(1),
                        Rank = rank++
                    });
                }
            }

            return result;
        }

        public void SaveContestLeaderboard(List<ContestLeaderrBoard> rows, IDbTransaction tx)
        {
            foreach (var row in rows)
            {
                const string sql = @"
                    MERGE ContestLeaderBoard AS target
                    USING (SELECT @PlayerID AS PlayerID, @ContestID AS ContestID) src
                    ON target.PlayerID = src.PlayerID AND target.ContestID = src.ContestID
                    WHEN MATCHED THEN
                        UPDATE SET TotalPoints = @TotalPoints, Rank = @Rank
                    WHEN NOT MATCHED THEN
                        INSERT (PlayerID, ContestID, TotalPoints, Rank)
                        VALUES (@PlayerID, @ContestID, @TotalPoints, @Rank);
                ";

                _repo.ExecuteNonQuery(sql, new
                {
                    row.PlayerID,
                    row.ContestID,
                    row.TotalPoints,
                    row.Rank
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
                MERGE GlobalLeaderBoard AS tgt
                USING Ranked src
                ON tgt.PlayerID = src.PlayerID
                WHEN MATCHED THEN
                    UPDATE SET TotalPoints = src.TotalPoints, Rank = src.RankVal
                WHEN NOT MATCHED THEN
                    INSERT (PlayerID, TotalPoints, Rank)
                    VALUES (src.PlayerID, src.TotalPoints, src.RankVal);
            ";

            _repo.ExecuteNonQuery(sql);
        }

        public void ClearContestLeaderboard(int contestId)
        {
            _repo.ExecuteNonQuery(
                "DELETE FROM ContestLeaderBoard WHERE ContestID = @ContestId",
                new { ContestId = contestId }
            );
        }

        public void ClearGlobalLeaderboard()
        {
            _repo.ExecuteNonQuery("DELETE FROM GlobalLeaderBoard");
        }
        public List<int> GetActiveContestIds()
        {
            const string sql = @"
        SELECT Contest_ID
        FROM Contest
        WHERE ContestStartDate <= @Now
          AND ContestEndDate >= @Now;
    ";

            var result = new List<int>();

            using (var reader = _repo.ExecuteReader(sql, new { Now = DateTime.UtcNow }))
            {
                while (reader.Read())
                {
                    result.Add(reader.GetInt32(0));
                }
            }

            return result;
        }
        public string ExportContestLeaderboard(
    int contestId,
    List<ContestLeaderrBoard> rows,
    string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(
                directoryPath,
                $"Leaderboard_Contest_{contestId}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv"
            );

            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("Rank,PlayerID,TotalPoints");

                foreach (var row in rows)
                {
                    writer.WriteLine($"{row.Rank},{row.PlayerID},{row.TotalPoints}");
                }
            }

            const string sql = @"
        INSERT INTO LeaderboardExportHistory
        (ContestID, FileLocation, ExportedAt)
        VALUES (@ContestID, @FileLocation, @ExportedAt);
    ";

            _repo.ExecuteNonQuery(sql, new
            {
                ContestID = contestId,
                FileLocation = filePath,
                ExportedAt = DateTime.UtcNow
            });

            return filePath;
        }

    }
}
