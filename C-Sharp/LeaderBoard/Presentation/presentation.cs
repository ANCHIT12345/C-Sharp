using LeaderBoard.Services;
using LeaderBoard.Models;
using LeaderBoard.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderBoard.Presentation
{
    public class presentation
    {
        public static void uiDisplay()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Game & Contest Leaderboard Scheduler ===");
                Console.WriteLine("1. Manage Games");
                Console.WriteLine("2. Manage Players");
                Console.WriteLine("3. Manage Contests");
                Console.WriteLine("4. Manage Leaderboard");
                Console.WriteLine("5. Submit Score (Manual/Json Import)");
                //Console.WriteLine("5. Simulate Player Scores (Multi-threaded)");
                //Console.WriteLine("6. Generate Leaderboard");
                //Console.WriteLine("7. Export Leaderboard to CSV");
                //Console.WriteLine("8. Start Scheduler (Auto Contest Start/End)");
                Console.WriteLine("9. Exit");
                Console.Write("Enter option: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        GameModule.ManageGames();
                        break;
                    case "2":
                        PlayerModule.ManagePlayers();
                        break;
                    case "3":
                        ContestModule.ManageContests();
                        break;
                    case "4":
                        LeaderboardModule.ManageLeaderboard();
                        break;
                    case "5":
                        var scoreModule = new ScoreModule();
                        scoreModule.ManageScore();
                        break;
                    case "6":
                        // Call method to generate leaderboard
                        break;
                    case "7":
                        // Call method to export leaderboard to CSV
                        break;
                    case "8":
                        // Call method to start scheduler
                        break;
                    case "9":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
