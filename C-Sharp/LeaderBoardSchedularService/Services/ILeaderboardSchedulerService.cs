using System.Threading;
using System.Threading.Tasks;

namespace Leaderboard.Services
{
    public interface ILeaderboardSchedulerService
    {
        void RunOnce();
        Task RunPeriodicAsync(int intervalSeconds, CancellationToken token);
    }
}