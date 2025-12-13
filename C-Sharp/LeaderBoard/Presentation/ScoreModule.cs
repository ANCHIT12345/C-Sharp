using LeaderBoard.Services;
using LeaderboardApp.Services;
using System;

namespace LeaderboardApp.UI
{
    public class ScoreModule
    {
        private readonly ScoreService _scoreService;

        public ScoreModule(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Score Import & Processing ===");
                Console.WriteLine("1. Import scores from JSON and process");
                Console.WriteLine("2. Back");
                Console.Write("Choice: ");
                var c = Console.ReadLine();

                switch (c)
                {
                    case "1":
                        Console.Write("Enter path to JSON file: ");
                        var path = Console.ReadLine();
                        try
                        {
                            _scoreService.ImportScoresFromJsonAndProcess(path);
                            Console.WriteLine("Import + processing completed.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        Pause();
                        break;
                    case "2": return;
                    default: Console.WriteLine("Invalid."); Pause(); break;
                }
            }
        }

        private static void Pause() { Console.WriteLine("Press Enter..."); Console.ReadLine(); }
    }
}
