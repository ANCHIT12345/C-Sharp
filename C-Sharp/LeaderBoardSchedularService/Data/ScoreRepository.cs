using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Leaderboard.Data
{
    public class ScoreRepository
    {
        private readonly DatabaseHelper _db;

        public ScoreRepository(DatabaseHelper db)
        {
            _db = db;
        }

        public int InsertScore(int playerId, decimal score, int? gameId)
        {
            const string sql = @"
                INSERT INTO PlayerScore (PlayerID, Score, GameID)
                VALUES (@PlayerID, @Score, @GameID);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return _db.ExecuteScalar<int>(sql, new
            {
                PlayerID = playerId,
                Score = score,
                GameID = gameId
            });
        }

        public int ImportFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var records = JsonSerializer.Deserialize<List<ScoreEventDto>>(json);

            int count = 0;

            foreach (var r in records)
            {
                InsertScore(r.PlayerId, r.PointsReceived, r.GameId);
                count++;
            }

            return count;
        }

        public List<(int PlayerId, decimal TotalPoints)> GetTotalsByGame(int gameId)
        {
            const string sql = @"
                SELECT PlayerID, SUM(Score)
                FROM PlayerScore
                WHERE GameID = @GameId
                GROUP BY PlayerID
                ORDER BY SUM(Score) DESC";

            var result = new List<(int, decimal)>();

            using var reader = _db.ExecuteReader(sql, new { GameId = gameId });
            while (reader.Read())
            {
                result.Add((reader.GetInt32(0), reader.GetDecimal(1)));
            }

            return result;
        }
    }

    public class ScoreEventDto
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public decimal PointsReceived { get; set; }
    }
}
