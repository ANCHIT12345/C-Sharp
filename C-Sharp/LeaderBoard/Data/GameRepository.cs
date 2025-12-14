using Leaderboard.Models;
using LeaderBoard.Data;
using LeaderBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Leaderboard.Data
{
    public class GameRepository
    {
        private readonly DatabaseHelper _db;

        public GameRepository()
        {
            _db = new DatabaseHelper();
        }

        public int Insert(Game game)
        {
            const string sql = @"
                INSERT INTO GameDetails (GameHeldDate, GameStartTime, GameEndTime, GameRoundsHeld, GameWinner, GameMVP, RunnerUp, BestTime, CtID, LtID)
                VALUES (@GameHeldDate,@GameStartTime,@GameEndTime,@GameRoundsHeld,@GameWinner,@GameMVP,@RunnerUp,@BestTime,@CtID,@LtID);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";
            return _db.ExecuteScalar<int>(sql, new
            {
                GameHeldDate = game.GameHeldDate?.Date,
                GameStartTime = game.GameStartTime,
                GameEndTime = game.GameEndTime,
                GameRoundsHeld = game.GameRoundsHeld,
                GameWinner = game.GameWinner,
                GameMVP = game.GameMVP,
                RunnerUp = game.RunnerUp,
                BestTime = game.BestTime,
                CtID = game.CtID,
                LtID = game.LtID
            });
        }
        public bool Update(Game game)
        {
            const string sql = @"
                UPDATE GameDetails SET
                  GameHeldDate=@GameHeldDate, GameStartTime=@GameStartTime, GameEndTime=@GameEndTime,
                  GameRoundsHeld=@GameRoundsHeld, GameWinner=@GameWinner, GameMVP=@GameMVP, RunnerUp=@RunnerUp,
                  BestTime=@BestTime, CtID=@CtID, LtID=@LtID
                WHERE Game_ID = @GameId;
            ";
            return _db.ExecuteNonQuery(sql, new
            {
                GameHeldDate = game.GameHeldDate?.Date,
                GameStartTime = game.GameStartTime,
                GameEndTime = game.GameEndTime,
                GameRoundsHeld = game.GameRoundsHeld,
                GameWinner = game.GameWinner,
                GameMVP = game.GameMVP,
                RunnerUp = game.RunnerUp,
                BestTime = game.BestTime,
                CtID = game.CtID,
                LtID = game.LtID,
                GameId = game.GameId
            }) > 0;
        }
        public bool Delete(int gameId)
        {
            const string sql = "DELETE FROM GameDetails WHERE Game_ID = @GameId;";
            return _db.ExecuteNonQuery(sql, new { GameId = gameId }) > 0;
        }

        public Game GetById(int gameId)
        {
            const string sql = @"
                SELECT Game_ID, GameHeldDate, GameStartTime, GameEndTime, GameRoundsHeld, GameWinner, GameMVP, RunnerUp, BestTime, CtID, LtID
                FROM GameDetails WHERE Game_ID = @GameId;
            ";
            using var rdr = _db.ExecuteReader(sql, new { GameId = gameId });
            if (rdr.Read())
            {
                return new Game
                {
                    GameId = rdr.GetInt32(0),
                    GameHeldDate = rdr.IsDBNull(1) ? (DateTime?)null : rdr.GetDateTime(1),
                    GameStartTime = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                    GameEndTime = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                    GameRoundsHeld = rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4),
                    GameWinner = rdr.IsDBNull(5) ? 0 : rdr.GetInt32(5),
                    GameMVP = rdr.IsDBNull(6) ? 0 : rdr.GetInt32(6),
                    RunnerUp = rdr.IsDBNull(7) ? 0 : rdr.GetInt32(7),
                    BestTime = rdr.IsDBNull(8) ? null : rdr.GetString(8),
                    CtID = rdr.IsDBNull(9) ? null : rdr.GetString(9),
                    LtID = rdr.IsDBNull(10) ? 0 : rdr.GetInt32(10)
                };
            }
            return null;
        }
        public List<Game> GetAll()
        {
            const string sql = @"
                SELECT Game_ID, GameHeldDate, GameStartTime, GameEndTime, GameRoundsHeld, GameWinner, GameMVP, RunnerUp, BestTime, CtID, LtID
                FROM GameDetails ORDER BY GameHeldDate DESC;
            ";
            var list = new List<Game>();
            using var rdr = _db.ExecuteReader(sql);
            while (rdr.Read())
            {
                list.Add(new Game
                {
                    GameId = rdr.GetInt32(0),
                    GameHeldDate = rdr.IsDBNull(1) ? (DateTime?)null : rdr.GetDateTime(1),
                    GameStartTime = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                    GameEndTime = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                    GameRoundsHeld = rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4),
                    GameWinner = rdr.IsDBNull(5) ? 0 : rdr.GetInt32(5),
                    GameMVP = rdr.IsDBNull(6) ? 0 : rdr.GetInt32(6),
                    RunnerUp = rdr.IsDBNull(7) ? 0 : rdr.GetInt32(7),
                    BestTime = rdr.IsDBNull(8) ? null : rdr.GetString(8),
                    CtID = rdr.IsDBNull(9) ? null : rdr.GetString(9),
                    LtID = rdr.IsDBNull(10) ? 0 : rdr.GetInt32(10)
                });
            }
            return list;
        }
        public int ImportFromJsonFile(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath)) throw new FileNotFoundException(jsonFilePath);
            var json = File.ReadAllText(jsonFilePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var games = JsonSerializer.Deserialize<List<GameJsonDto>>(json, options);
            if (games == null) return 0;

            int changed = 0;
            foreach (var dto in games)
            {
                // Map DTO to Game
                var g = new Game
                {
                    GameId = dto.GameId,
                    GameHeldDate = dto.GameHeldDate,
                    GameStartTime = dto.GameStartTime,
                    GameEndTime = dto.GameEndTime,
                    GameRoundsHeld = dto.GameRoundsHeld,
                    GameWinner = dto.GameWinner,
                    GameMVP = dto.GameMVP,
                    RunnerUp = dto.RunnerUp,
                    BestTime = dto.BestTime,
                    CtID = dto.CtID,
                    LtID = dto.LtID
                };

                if (g.GameId > 0)
                {
                    var existing = GetById(g.GameId);
                    if (existing == null) { Insert(g); changed++; }
                    else { Update(g); changed++; }
                }
                else
                {
                    Insert(g);
                    changed++;
                }
            }
            return changed;
        }
        private class GameJsonDto
        {
            public int GameId { get; set; } // optional; if present, we upsert by id
            public DateTime? GameHeldDate { get; set; }
            public string GameStartTime { get; set; }
            public string GameEndTime { get; set; }
            public int GameRoundsHeld { get; set; }
            public int GameWinner { get; set; }
            public int GameMVP { get; set; }
            public int RunnerUp { get; set; }
            public string BestTime { get; set; }
            public string CtID { get; set; }
            public int LtID { get; set; }
        }
    }
}