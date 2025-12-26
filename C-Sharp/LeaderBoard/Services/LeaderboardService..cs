using Leaderboard.Data;
using LeaderBoard.Data;
using LeaderBoard.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaderboard.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IRepository _repo;
        private readonly LeaderboardRepository _leaderboardRepo;

        public LeaderboardService(IRepository repository)
        {
            _repo = repository;
            _leaderboardRepo = new LeaderboardRepository(repository);
        }

        public List<ContestLeaderrBoard> GenerateLeaderboard(int contestId)
        {
            if (contestId <= 0)
                throw new ArgumentException("Invalid contest id");

            using (var tx = _repo.BeginTransaction())
            {
                try
                {
                    _leaderboardRepo.ClearContestLeaderboard(contestId);

                    var rows = _leaderboardRepo.GenerateContestLeaderboard(contestId);

                    _leaderboardRepo.SaveContestLeaderboard(rows, tx);
                    _leaderboardRepo.UpdateGlobalLeaderboard(tx);

                    _repo.CommitTransaction(tx);
                    return rows;
                }
                catch
                {
                    _repo.RollbackTransaction(tx);
                    throw;
                }
            }
        }

        public void DisplayLeaderboard(int contestId)
        {
            var rows = GenerateLeaderboard(contestId);

            Console.WriteLine($"Leaderboard for Contest {contestId}");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Rank | PlayerID | Total Points");

            foreach (var row in rows)
            {
                Console.WriteLine($"{row.Rank,4} | {row.PlayerID,8} | {row.TotalPoints,12}");
            }
        }

        public string ExportLeaderboardToCsv(int contestId, string directoryPath)
        {
            var rows = GenerateLeaderboard(contestId);

            return _leaderboardRepo.ExportContestLeaderboard(
                contestId,
                rows,
                directoryPath
            );
        }

        public async Task PeriodicRefreshAsync(int intervalSeconds, CancellationToken cancellationToken)
        {
            if (intervalSeconds <= 0)
                throw new ArgumentException("Interval must be greater than zero.");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var activeContests = _leaderboardRepo.GetActiveContestIds();

                    if (activeContests.Count == 0)
                    {
                        Console.WriteLine("[Scheduler] No active contests found.");
                    }

                    foreach (var contestId in activeContests)
                    {
                        try
                        {
                            Console.WriteLine($"[Scheduler] Updating leaderboard for Contest {contestId}");
                            GenerateLeaderboard(contestId);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ERROR] Contest {contestId}: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Scheduler ERROR] {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(intervalSeconds), cancellationToken);
            }
        }

    }
}
