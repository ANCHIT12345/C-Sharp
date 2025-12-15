using Leaderboard.Data;
using Leaderboard.Services;
using LeaderBoard.Data;
using LeaderBoard.Models;
using LeaderBoard.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace LeaderBoard.Services
{
    public class ScoreJsonDto
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public decimal PointsReceived { get; set; }
    }
    public class ScoreService : IScoreService
    {
        private readonly ScoreRepository _repo;
        private readonly object _rankLock = new object();
        private readonly ConcurrentDictionary<int, Score> _scoreCache;
        public ScoreService()
        {
            _repo = new ScoreRepository();
            _scoreCache = new ConcurrentDictionary<int, Score>();
        }
        public int SubmitScore(Score score)
        {
            if (score == null) throw new ArgumentNullException(nameof(score));
            if (!score.Validate()) return 0;
            var id = _repo.InsertScore(score.PlayerId, score.Points, score.ContestId == 0 ? (int?)null : score.ContestId);
            return id;
        }
    }
}

//public bool SubmitScore(Score score)
//{
//    if (score == null) throw new ArgumentNullException(nameof(score));
//    if (!score.Validate()) return false;
//    IDbTransaction tx = null;
//    try
//    {
//        tx = _repo.BeginTransaction();
//        const string sql = @"
//                    INSERT INTO PlayerScore (PlayerID, Score, GameID, [Rank])
//                    VALUES (@PlayerId, @Points, @GameId, @Rank);
//                    SELECT CAST(SCOPE_IDENTITY() AS INT);
//                ";
//        var conn = _repo.Connection;
//        using (var cmd = conn.CreateCommand())
//        {
//            cmd.CommandText = sql;
//            cmd.CommandType = CommandType.Text;
//            cmd.Transaction = tx;
//            var param = cmd.CreateParameter();
//            param.ParameterName = "@PlayerId";
//            param.Value = score.PlayerId;
//            cmd.Parameters.Add(param);

//            param = cmd.CreateParameter();
//            param.ParameterName = "@Points";
//            param.Value = score.Points;
//            cmd.Parameters.Add(param);

//            param = cmd.CreateParameter();
//            param.ParameterName = "@GameId";
//            param.Value = score.ContestId == 0 ? (object)DBNull.Value : score.ContestId;
//            cmd.Parameters.Add(param);

//            param = cmd.CreateParameter();
//            param.ParameterName = "@Rank";
//            param.Value = DBNull.Value;
//            cmd.Parameters.Add(param);

//            var result = cmd.ExecuteScalar();
//        }
//        _repo.CommitTransaction(tx);
//        tx = null;
//        return true;
//    }
//    catch (Exception ex)
//    {
//        try { _repo.RollbackTransaction(tx); } catch { }
//        Console.Error.WriteLine($"Error submitting score: {ex.Message}");
//        return false;
//    }
//    finally
//    {
//        tx?.Dispose();
//    }
//}
//public async Task SimulatePlayerScoresAsync(int contestId, int[] playerIds, int submissionsPerPlayer = 5, int maxDegreeOfParallelism = 10)
//{
//    if (playerIds == null || playerIds.Length == 0) throw new ArgumentException("playerIds required");
//    using (var throttler = new SemaphoreSlim(maxDegreeOfParallelism))
//    {
//        var task = new ConcurrentBag<Task>();
//        var rnd = new Random();
//        foreach (var pid in playerIds)
//        {
//            for (int s = 0; s < submissionsPerPlayer; s++)
//            {
//                await throttler.WaitAsync().ConfigureAwait(false);
//                var t = Task.Run(async () =>
//                {
//                    try
//                    {
//                        var points = Math.Round((decimal)(rnd.NextDouble() * 1000.0), 2);
//                        var score = new Score
//                        {
//                            PlayerId = pid,
//                            ContestId = contestId,
//                            Points = points,
//                            SubmittedAt = DateTime.UtcNow
//                        };
//                        await Task.Delay(rnd.Next(30, 1200)).ConfigureAwait(false);
//                        await _concurrencyShemaphore.WaitAsync().ConfigureAwait(false);
//                        try
//                        {
//                            var ok = SubmitScore(score);
//                            if (!ok)
//                            {
//                                Console.Error.WriteLine($"Failed to submit score for PlayerID {pid}");
//                            }
//                        }
//                        finally
//                        {
//                            _concurrencyShemaphore.Release();
//                        }
//                    }
//                    finally
//                    {
//                        throttler.Release();
//                    }
//                });
//                task.Add(t);
//            }
//        }
//        await Task.WhenAll(task).ConfigureAwait(false);
//    }
//}
//public int ImportScoresFromJsonAndProcess(string jsonPath)
//{
//    if (string.IsNullOrWhiteSpace(jsonPath))
//        throw new ArgumentException("JSON path is required");

//    if (!File.Exists(jsonPath))
//        throw new FileNotFoundException("JSON file not found", jsonPath);

//    var json = File.ReadAllText(jsonPath);
//    var options = new JsonSerializerOptions
//    {
//        PropertyNameCaseInsensitive = true
//    };

//    var items = JsonSerializer.Deserialize<List<ScoreJsonDto>>(json, options);
//    if (items == null || items.Count == 0)
//        return 0;

//    int inserted = 0;

//    foreach (var dto in items)
//    {
//        var score = new Score
//        {
//            PlayerId = dto.PlayerId,
//            ContestId = dto.GameId,
//            Points = dto.PointsReceived,
//            SubmittedAt = dto.DateTime
//        };

//        if (SubmitScore(score))
//            inserted++;
//    }
//    RecalculateGlobalLeaderboard();
//    RecalculateContestLeaderboards();

//    return inserted;
//}
//private void RecalculateGlobalLeaderboard()
//{
//    lock (_rankLock)
//    {
//        const string sql = @"
//            WITH Ranked AS (
//                SELECT PlayerID,
//                       SUM(Score) AS TotalPoints,
//                       DENSE_RANK() OVER (ORDER BY SUM(Score) DESC) AS RankVal
//                FROM PlayerScore
//                GROUP BY PlayerID
//            )
//            MERGE GlobalLeaderBoard AS t
//            USING Ranked AS s
//            ON t.PlayerID = s.PlayerID
//            WHEN MATCHED THEN
//              UPDATE SET TotalPoints = s.TotalPoints, Rank = s.RankVal
//            WHEN NOT MATCHED THEN
//              INSERT (PlayerID, TotalPoints, Rank)
//              VALUES (s.PlayerID, s.TotalPoints, s.RankVal);
//        ";
//        _repo.ExecuteNonQuery(sql);
//    }
//}
//private void RecalculateContestLeaderboards()
//{
//    lock (_rankLock)
//    {
//        const string sql = @"
//            WITH Ranked AS (
//                SELECT PlayerID,
//                       GameID,
//                       SUM(Score) AS TotalPoints,
//                       DENSE_RANK() OVER (PARTITION BY GameID ORDER BY SUM(Score) DESC) AS RankVal
//                FROM PlayerScore
//                WHERE GameID IS NOT NULL
//                GROUP BY PlayerID, GameID
//            )
//            MERGE ContestLeaderBoard AS t
//            USING Ranked AS s
//            ON t.PlayerID = s.PlayerID AND t.ContestID = s.GameID
//            WHEN MATCHED THEN
//              UPDATE SET TotalPoints = s.TotalPoints, Rank = s.RankVal
//            WHEN NOT MATCHED THEN
//              INSERT (PlayerID, ContestID, TotalPoints, Rank)
//              VALUES (s.PlayerID, s.GameID, s.TotalPoints, s.RankVal);
//        ";
//        _repo.ExecuteNonQuery(sql);
//    }
//}
//private void UpdatePlayerRatings()
//{
//    const string sql = @"
//        UPDATE P
//        SET Rating = ISNULL(Rating, 1000) + (100 - (GLB.Rank * 10))
//        FROM PlayerScore P
//        JOIN GlobalLeaderBoard GLB ON GLB.PlayerID = P.UserID;
//    ";

//    _repo.ExecuteNonQuery(sql);
//}



//public Task SimulatePlayerScoreAsync(int contestId, int[] playerIds, int submissionsPerPlayer = 5, int maxDegreeOfParallelism = 10)
//{
//    throw new NotImplementedException();
//}