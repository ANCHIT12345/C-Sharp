using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderBoard.Models
{
    public class Score
    {
        public int ScoreID { get; set; }
        public int PlayerId { get; set; }
        public int ContestId { get; set; }
        public decimal Points { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public bool Validate() => Points >= 0;
    }
}
