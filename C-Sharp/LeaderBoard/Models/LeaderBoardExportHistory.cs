using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderBoard.Models
{
    public class LeaderBoardExportHistory
    {
        public int ExportId { get; set; }
        public int? GameID { get; set; }
        public int? ContestID { get; set; }
        public string FileLocation { get; set; }
        public DateTime ExportedAt { get; set; } = DateTime.UtcNow;
    }
}
