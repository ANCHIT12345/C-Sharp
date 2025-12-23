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
                throw new ArgumentException(nameof(contestId));

            using (var tx = _repo.BeginTransaction())
            {
                try
                {
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
            Console.WriteLine("Rank | PlayerID | Points");

            foreach (var r in rows)
                Console.WriteLine($"{r.Rank,4} | {r.PlayerID,8} | {r.TotalPoints,10:N2}");
        }

        public string ExportLeaderboardToCsv(int contestId, string directory)
        {
            var rows = GenerateLeaderboard(contestId);
            return _leaderboardRepo.ExportContestLeaderboard(contestId, rows, directory);
        }

        public async Task PeriodicRefreshAsync(int seconds, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    // fetch active contests elsewhere
                }
                catch { }

                await Task.Delay(TimeSpan.FromSeconds(seconds), token);
            }
        }
    }
}
