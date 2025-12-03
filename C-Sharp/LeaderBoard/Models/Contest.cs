using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderBoard.Models
{
    public class Contest
    {
        public int ContestId { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
        public string CtID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public int LtID { get; set; }
        public bool IsOngoing(DateTime now) => IsActive && now >= StartTime && now < EndTime;
        public TimeSpan GetDuration() => EndTime - StartTime;
    }
}
