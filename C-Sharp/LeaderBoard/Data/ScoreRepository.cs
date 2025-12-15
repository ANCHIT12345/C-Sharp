using LeaderBoard.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Json;

namespace Leaderboard.Data
{
    public class ScoreRepository
    {
        private readonly DatabaseHelper _db;
        private readonly object _rankLock;

        public ScoreRepository()
        {
            _db = new DatabaseHelper();
        }
        public int InsertScore(int playerId, decimal pointsReceived, int? gameId)
        {
            const string sql = @"
                INSERT INTO PlayerScore (PlayerID, Score, GameID)
                VALUES (@PlayerID, @Score, @GameID);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return _db.ExecuteScalar<int>(sql, new
            {
                PlayerID = playerId,
                Score = pointsReceived,
                GameID = gameId
            });
        }
        public int BulkInsertFromJsonFile(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath)) throw new FileNotFoundException(jsonFilePath);

            var json = File.ReadAllText(jsonFilePath);
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var list = JsonSerializer.Deserialize<List<ScoreEventDto>>(json, opts);
            if (list == null || list.Count == 0) return 0;

            int inserted = 0;
            using (var tx = _db.BeginTransaction())
            {
                try
                {
                    foreach (var dto in list)
                    {
                        var dataJson = JsonSerializer.Serialize(dto); // store raw event for audit if needed
                        var id = InsertScore(dto.PlayerId, dto.PointsReceived, dto.GameId == 0 ? (int?)null : dto.GameId);
                        if (id > 0) inserted++;
                    }
                    _db.CommitTransaction(tx);
                }
                catch
                {
                    _db.RollbackTransaction(tx);
                    throw;
                }
            }
            return inserted;
        }
        public List<(int PlayerId, decimal TotalPoints)> GetAggregatedPointsByGame(int gameId)
        {
            const string sql = @"
                SELECT PlayerID, SUM(Score) AS TotalPoints
                FROM PlayerScore
                WHERE GameID = @GameID
                GROUP BY PlayerID
                ORDER BY TotalPoints DESC;
            ";
            var outList = new List<(int, decimal)>();
            using (var rdr = _db.ExecuteReader(sql, new { GameID = gameId }))
            {
                while (rdr.Read())
                {
                    outList.Add((rdr.GetInt32(0), rdr.GetDecimal(1)));
                }
            }
            return outList;
        }
        public List<(int PlayerId, decimal TotalPoints)> GetAggregatedGlobalPoints()
        {
            const string sql = @"
                SELECT PlayerID, SUM(Score) AS TotalPoints
                FROM PlayerScore
                GROUP BY PlayerID
                ORDER BY TotalPoints DESC;
            ";
            var outList = new List<(int, decimal)>();
            using (var rdr = _db.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    outList.Add((rdr.GetInt32(0), rdr.GetDecimal(1)));
                }
            }
            return outList;
        }
        // Add inside ScoreRepository class
        public void UpsertGlobalTotal(int playerId, decimal totalPoints)
        {
            const string sql = @"
        IF EXISTS (SELECT 1 FROM GlobalLeaderBoard WHERE PlayerID = @PlayerId)
           UPDATE GlobalLeaderBoard SET TotalPoints = @TotalPoints WHERE PlayerID = @PlayerId;
        ELSE
           INSERT INTO GlobalLeaderBoard (PlayerID, TotalPoints, Rank) VALUES (@PlayerId, @TotalPoints, 0);
    ";
            _db.ExecuteNonQuery(sql, new { PlayerId = playerId, TotalPoints = totalPoints });
        }
        public void RecalculateGlobalLeaderboard()
        {
            lock (_rankLock)
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
        }
        public void RecalculateContestLeaderboards()
        {
            lock (_rankLock)
            {
                const string sql = @"
                    WITH Ranked AS (
                        SELECT PlayerID,
                               GameID,
                               SUM(Score) AS TotalPoints,
                               DENSE_RANK() OVER (PARTITION BY GameID ORDER BY SUM(Score) DESC) AS RankVal
                        FROM PlayerScore
                        WHERE GameID IS NOT NULL
                        GROUP BY PlayerID, GameID
                    )
                    MERGE ContestLeaderBoard AS t
                    USING Ranked AS s
                    ON t.PlayerID = s.PlayerID AND t.ContestID = s.GameID
                    WHEN MATCHED THEN
                      UPDATE SET TotalPoints = s.TotalPoints, Rank = s.RankVal
                    WHEN NOT MATCHED THEN
                      INSERT (PlayerID, ContestID, TotalPoints, Rank)
                      VALUES (s.PlayerID, s.GameID, s.TotalPoints, s.RankVal);
                ";

                _db.ExecuteNonQuery(sql);
            }
        }
        public void UpdatePlayerRatings()
        {
            const string sql = @"
                UPDATE P
                SET Rating = ISNULL(Rating, 1000) + (100 - (GLB.Rank * 10))
                FROM PlayerScore P
                JOIN GlobalLeaderBoard GLB ON GLB.PlayerID = P.UserID;
            ";

            _db.ExecuteNonQuery(sql);
        }
    }
    public class ScoreEventDto
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public decimal TotalPoints { get; set; }
        public DateTime? EventAt { get; set; }
        public decimal PointsReceived { get; set; }
        public int? ContestId { get; set; }
    }
}
