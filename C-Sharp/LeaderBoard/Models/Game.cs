using System;

namespace Leaderboard.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public DateTime? GameHeldDate { get; set; }
        public string GameStartTime { get; set; }
        public string GameEndTime { get; set; }
        public int GameRoundsHeld { get; set; }
        public int GameWinner { get; set; }
        public int GameMVP { get; set; }
        public int RunnerUp { get; set; }
        public string BestTime { get; set; } 
        public string CtID { get; set; }  
        public int LtID { get; set; }   
        public override string ToString()
            => $"{GameId}: {GameHeldDate?.ToShortDateString()} {GameStartTime}-{GameEndTime} rounds:{GameRoundsHeld}";
    }
}
