using System.Collections.Generic;

namespace Leaderboard.Data
{
    public interface ILeaderboardRepository
    {
        void RecalculateGlobalLeaderboard();
        void RecalculateContestLeaderboard(int contestId);
        List<int> GetActiveContests();
    }
}