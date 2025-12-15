using LeaderBoard.Models;
using LeaderBoard.Services;
using System;

namespace LeaderBoard.Presentation
{
    public class ScoreModule
    {
        private readonly ScoreService _scoreService;

        public ScoreModule()
        {
            _scoreService = new ScoreService();
        }

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
                        SubmitScoreManually();
                        break;

                    case "2":
                        ImportFromJson();
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
        private void SubmitScoreManually()
        {
            try
            {
                Console.Write("Player ID: ");
                int playerId = int.Parse(Console.ReadLine());

                Console.Write("Game / Contest ID (0 if none): ");
                int contestId = int.Parse(Console.ReadLine());

                Console.Write("Points: ");
                decimal points = decimal.Parse(Console.ReadLine());

                var score = new Score
                {
                    PlayerId = playerId,
                    ContestId = contestId,
                    Points = points
                };

                int scoreId = _scoreService.SubmitScore(score);

                if (scoreId > 0)
                    Console.WriteLine($"Score submitted successfully (ScoreID = {scoreId})");
                else
                    Console.WriteLine("Score submission failed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Pause();
        }

        private void ImportFromJson()
        {
            try
            {
                Console.Write("Enter path to JSON file: ");
                var path = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(path))
                {
                    Console.WriteLine("Invalid path.");
                    Pause();
                    return;
                }

                // Direct repository access is intentional here
                var repo = new Leaderboard.Data.ScoreRepository();
                int count = repo.BulkInsertFromJsonFile(path);

                // Recalculate leaderboard + ratings
                repo.RecalculateGlobalLeaderboard();
                repo.RecalculateContestLeaderboards();
                repo.UpdatePlayerRatings();

                Console.WriteLine($"{count} scores imported and processed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
