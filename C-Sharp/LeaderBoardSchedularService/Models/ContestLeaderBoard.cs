namespace Leaderboard.Models
{
    public class ContestLeaderrBoard
    {
        public int CLB_ID { get; set; }
        public int PlayerID { get; set; }
        public int ContestID { get; set; }
        public decimal TotalPoints { get; set; }
        public int Rank { get; set; }
    }
}
