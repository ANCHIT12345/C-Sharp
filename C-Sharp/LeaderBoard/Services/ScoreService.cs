using Leaderboard.Data;
using Leaderboard.Services;
using LeaderBoard.Data;
using LeaderBoard.Models;
using LeaderBoard.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace LeaderBoard.Services
{
    public class ScoreJsonDto
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public decimal PointsReceived { get; set; }
    }
    public class ScoreService : IScoreService
    {
        private readonly ScoreRepository _repo;
        private readonly object _rankLock = new object();
        private readonly ConcurrentDictionary<int, Score> _scoreCache;
        public ScoreService()
        {
            _repo = new ScoreRepository();
            _scoreCache = new ConcurrentDictionary<int, Score>();
        }
        public int SubmitScore(Score score)
        {
            if (score == null) throw new ArgumentNullException(nameof(score));
            if (!score.Validate()) return 0;
            var id = _repo.InsertScore(score.PlayerId, score.Points, score.ContestId == 0 ? (int?)null : score.ContestId);
            return id;
        }
        public void SubmitScoreManually()
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

                int scoreId = SubmitScore(score);

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

        public void ImportFromJson()
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
                //repo.RecalculateGlobalLeaderboard();
                //repo.RecalculateContestLeaderboards();
                //repo.UpdatePlayerRatings();

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

