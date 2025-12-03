using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderBoard.Models
{
    public class ApplicationLog
    {
        public int LogID    { get; set; }
        public string LogType  { get; set; }
        public string Message  { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
