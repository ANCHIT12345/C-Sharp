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
        int SubmitScore(Score score);
    }
}
