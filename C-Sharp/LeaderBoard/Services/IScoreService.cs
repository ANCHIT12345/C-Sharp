using LeaderBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderBoard.Services
{
    public interface IScoreService
    {
        bool SubmitScore(Score score);
        Task SimulatePlayerScoreAsync(int contestId, int[] playerIds, int submissionsPerPlayer = 5, int maxDegreeOfParallelism = 10);
    }
}
