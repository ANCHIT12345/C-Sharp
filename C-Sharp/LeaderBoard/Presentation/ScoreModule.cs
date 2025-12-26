using LeaderBoard.Models;
using LeaderBoard.Services;
using System;

namespace LeaderBoard.Presentation
{
    public class ScoreModule
    {
       public static ScoreService sm = new ScoreService();

        public void ManageScore()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Score Management ===");
                Console.WriteLine("1. Submit score manually");
                Console.WriteLine("2. Import scores from JSON");
                Console.WriteLine("3. Back");
                Console.Write("Choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        sm.SubmitScoreManually();
                        break;

                    case "2":
                        sm.ImportFromJson();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }

        }
        private void Pause()
        {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
