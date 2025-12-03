using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderBoard.Models
{
    public class ContestLeaderrBoard
    {
        public int CLB_ID { get; set; }
        public int PlayerID { get; set; }
        public int ContestID { get; set; }
        public decimal TotalPoints { get; set; }
        public int Rank { get; set; }
        public void UpdateRank(int newRank)=> Rank = newRank;
    }
}
