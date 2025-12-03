using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using LeaderBoard.Models;
using LeaderBoard.Data;
using System.Collections.Concurrent;

namespace LeaderBoard.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IRepository _repo;
        private readonly object _rankLock = new object();
        private readonly SemaphoreSlim _concurrencyShemaphore;

        public ScoreService(IRepository repository, int maxConcurrency = 20)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _concurrencyShemaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);
        }
        public bool SubmitScore(Score score)
        {
            if(score == null) throw new ArgumentNullException(nameof(score));
            if (!score.Validate()) return false;
            IDbTransaction tx = null;
            try
            {
                tx = _repo.BeginTransaction();
                const string sql = @"
                    INSERT INTO PlayerScore (PlayerID, Score, GameID, [Rank])
                    VALUES (@PlayerId, @Points, @GameId, @Rank);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);
                ";
                var conn = _repo.Connection;
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = tx;
                    var param = cmd.CreateParameter();
                    param.ParameterName = "@PlayerId";
                    param.Value = score.PlayerId;
                    cmd.Parameters.Add(param);

                    param = cmd.CreateParameter();
                    param.ParameterName = "@Points";
                    param.Value = score.Points;
                    cmd.Parameters.Add(param);

                    param = cmd.CreateParameter();
                    param.ParameterName = "@GameId";
                    param.Value = score.ContestId == 0 ? (object)DBNull.Value : score.ContestId;
                    cmd.Parameters.Add(param);

                    param = cmd.CreateParameter();
                    param.ParameterName = "@Rank";
                    param.Value = DBNull.Value;
                    cmd.Parameters.Add(param);

                    var result = cmd.ExecuteScalar();
                }
                _repo.CommitTransaction(tx);
                tx = null;
                return true;
            }
            catch (Exception ex)
            {
                try { _repo.RollbackTransaction(tx); } catch { }
                Console.Error.WriteLine($"Error submitting score: {ex.Message}");
                return false;
            }
            finally
            {
                tx?.Dispose();
            }
        }
        public async Task SimulatePlayerScoresAsync(int contestId, int[] playerIds, int submissionsPerPlayer = 5, int maxDegreeOfParallelism = 10)
        {
            if (playerIds == null || playerIds.Length == 0) throw new ArgumentException("playerIds required");
            using (var throttler = new SemaphoreSlim(maxDegreeOfParallelism))
            {
                var task = new ConcurrentBag<Task>();
                var rnd = new Random();
                foreach (var pid in playerIds)
                {
                    for (int s = 0; s < submissionsPerPlayer; s++)
                    {
                        await throttler.WaitAsync().ConfigureAwait(false);
                        var t = Task.Run(async () =>
                        {
                            try
                            {
                                var points = Math.Round((decimal)(rnd.NextDouble() * 1000.0), 2);
                                var score = new Score
                                {
                                    PlayerId = pid,
                                    ContestId = contestId,
                                    Points = points,
                                    SubmittedAt = DateTime.UtcNow;
                                };
                                await Task,Delay(rnd.Next(30, 1200)).ConfigureAwait(false);
                                await _concurrencyShemaphore.WaitAsync().ConfigureAwait(false);
                                try
                                {
                                    var ok = SubmitScore(score);
                                    if (!ok)
                                    {
                                        Console.Error.WriteLine($"Failed to submit score for PlayerID {pid}");
                                    }
                                }
                                finally
                                {
                                    _concurrencyShemaphore.Release();
                                }
                            }
                            finally
                            {
                                throttler.Release();
                            }
                        });
                        task.Add(t);
                    }
                }
                await Task.WhenAll(task).ConfigureAwait(false);
            }
        }

        public Task SimulatePlayerScoreAsync(int contestId, int[] playerIds, int submissionsPerPlayer = 5, int maxDegreeOfParallelism = 10)
        {
            throw new NotImplementedException();
        }
    }
}
