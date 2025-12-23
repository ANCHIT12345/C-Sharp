using LeaderBoard.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaderboard.Services
{
    public interface ILeaderboardService
    {
        List<ContestLeaderrBoard> GenerateLeaderboard(int contestId);

        void DisplayLeaderboard(int contestId);

        string ExportLeaderboardToCsv(int contestId, string directoryPath);

        Task PeriodicRefreshAsync(int intervalSeconds, CancellationToken cancellationToken);
    }
}
