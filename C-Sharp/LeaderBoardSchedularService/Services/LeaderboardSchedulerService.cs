using Leaderboard.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leaderboard.Services
{
    public class LeaderboardSchedulerService : ILeaderboardSchedulerService
    {
        private readonly ILeaderboardRepository _repository;

        public LeaderboardSchedulerService(ILeaderboardRepository repository)
        {
            _repository = repository;
        }

        public void RunOnce()
        {
            Console.WriteLine("Running leaderboard scheduler...");

            _repository.RecalculateGlobalLeaderboard();

            var contests = _repository.GetActiveContests();
            foreach (var contestId in contests)
            {
                _repository.RecalculateContestLeaderboard(contestId);
            }

            Console.WriteLine("Leaderboard update completed.");
        }

        public async Task RunPeriodicAsync(int intervalSeconds, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    RunOnce();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Scheduler error: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(intervalSeconds), token);
            }
        }
    }
}
