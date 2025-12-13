using LeaderBoard.Models;
using LeaderboardApp.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LeaderboardApp.Services
{
    public interface ILeaderboardService
    {

        List<ContestLeaderrBoard> GenerateLeaderboard(int contestId);
        void DisplayLeaderboard(int contestId, List<ContestLeaderrBoard> rows = null);

        string ExportLeaderboardToCsv(int contestId, string directoryPath);

        Task PeriodicRefreshAsync(int intervalSeconds, CancellationToken cancellationToken);
    }
}
