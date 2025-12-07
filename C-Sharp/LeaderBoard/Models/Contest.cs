using System;

namespace LeaderboardApp.Models
{
    public class Contest
    {
        public int ContestId { get; set; }
        public string CtID { get; set; } 
        public int LtID { get; set; }
        public int Winner { get; set; } 
        public int MVP_OF_Contest { get; set; } 
        public int Runner_UP { get; set; } 
        public int TotalNumberOfMatches { get; set; }
        public string Best_Time { get; set; }  
        public DateTime? ContestStartDate { get; set; }
        public DateTime? ContestEndDate { get; set; }
        public bool IsActive => ContestStartDate.HasValue && ContestEndDate.HasValue
                                && DateTime.UtcNow >= ContestStartDate.Value && DateTime.UtcNow <= ContestEndDate.Value;
        public override string ToString()
            => $"Contest {ContestId}: {CtID} ({ContestStartDate?.ToShortDateString()} - {ContestEndDate?.ToShortDateString()})";
    }
}
