using System.Collections.Generic;
using System.Data;

namespace Leaderboard.Data
{
    public class LeaderboardRepository : ILeaderboardRepository
    {
        private readonly DatabaseHelper _db;

        public LeaderboardRepository(DatabaseHelper db)
        {
            _db = db;
        }

        public List<int> GetActiveContests()
        {
            const string sql = @"
                SELECT Contest_ID
                FROM Contest
                WHERE ContestStartDate <= GETDATE()
                  AND ContestEndDate >= GETDATE();
            ";

            var result = new List<int>();

            using (var reader = _db.ExecuteReader(sql))
            {
                while (reader.Read())
                    result.Add(reader.GetInt32(0));
            }

            return result;
        }

        public void RecalculateGlobalLeaderboard()
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

            _db.ExecuteNonQuery(sql);
        }

        public void RecalculateContestLeaderboard(int contestId)
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
                MERGE ContestLeaderBoard AS t
                USING Ranked AS s
                ON t.PlayerID = s.PlayerID AND t.ContestID = @ContestId
                WHEN MATCHED THEN
                    UPDATE SET TotalPoints = s.TotalPoints, Rank = s.RankVal
                WHEN NOT MATCHED THEN
                    INSERT (PlayerID, ContestID, TotalPoints, Rank)
                    VALUES (s.PlayerID, @ContestId, s.TotalPoints, s.RankVal);
            ";

            _db.ExecuteNonQuery(sql, new { ContestId = contestId });
        }
    }
}
