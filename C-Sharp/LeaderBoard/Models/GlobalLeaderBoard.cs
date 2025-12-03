using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderBoard.Models
{
    public class GlobalLeaderBoard
    {
        public int GLB_ID { get; set; }
        public int PlayerID { get; set; }
        public decimal TotalPoints { get; set; }
        public int Rank { get; set; }
    }
}
