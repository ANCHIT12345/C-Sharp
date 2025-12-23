using System;
using System.Threading;
using System.Threading.Tasks;
using Leaderboard.Services;
using LeaderBoard.Data;

namespace LeaderBoard.Presentation
{
    public class LeaderboardModule
    {
        private readonly ILeaderboardService _leaderboardService;
        private readonly RatingService _ratingService;

        private CancellationTokenSource _cts;
        private Task _periodicTask;

        public LeaderboardModule()
        {
            var repo = new DatabaseHelper();
            _leaderboardService = new LeaderboardService(repo);
            _ratingService = new RatingService();
        }

        public void ManageLeaderboard()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Leaderboard ===");
                Console.WriteLine("1. Generate & Display Leaderboard");
                Console.WriteLine("2. Export Leaderboard to CSV");
                Console.WriteLine("3. Generate & Update Ratings");
                Console.WriteLine("4. Start Periodic Auto-refresh");
                Console.WriteLine("5. Stop Auto-refresh");
                Console.WriteLine("6. Back");
                Console.Write("Choice: ");

                var c = Console.ReadLine();

                switch (c)
                {
                    case "1":
                        GenerateAndDisplay();
                        break;

                    case "2":
                        Export();
                        break;

                    case "3":
                        GenerateAndUpdateRatings();
                        break;

                    case "4":
                        StartPeriodicRefresh();
                        break;

                    case "5":
                        StopPeriodicRefresh();
                        break;

                    case "6":
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        private void GenerateAndDisplay()
        {
            Console.Write("Contest ID: ");
            if (!int.TryParse(Console.ReadLine(), out int cid))
            {
                Console.WriteLine("Invalid Contest ID");
                Pause();
                return;
            }

            _leaderboardService.DisplayLeaderboard(cid);
            Pause();
        }

        private void Export()
        {
            Console.Write("Contest ID: ");
            if (!int.TryParse(Console.ReadLine(), out int cid))
            {
                Console.WriteLine("Invalid Contest ID");
                Pause();
                return;
            }

            Console.Write("Directory (default ./exports): ");
            var dir = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(dir))
                dir = "./exports";

            var path = _leaderboardService.ExportLeaderboardToCsv(cid, dir);
            Console.WriteLine($"Exported to: {path}");
            Pause();
        }

        private void GenerateAndUpdateRatings()
        {
            Console.Write("Contest ID: ");
            if (!int.TryParse(Console.ReadLine(), out int cid))
            {
                Console.WriteLine("Invalid Contest ID");
                Pause();
                return;
            }

            var rows = _leaderboardService.GenerateLeaderboard(cid);
            _ratingService.UpdateRatingsForContest(rows);

            Console.WriteLine("Ratings updated successfully.");
            Pause();
        }

        private void StartPeriodicRefresh()
        {
            if (_cts != null)
            {
                Console.WriteLine("Periodic refresh already running.");
                Pause();
                return;
            }

            Console.Write("Interval seconds (default 10): ");
            if (!int.TryParse(Console.ReadLine(), out int interval))
                interval = 10;

            _cts = new CancellationTokenSource();
            _periodicTask = _leaderboardService.PeriodicRefreshAsync(interval, _cts.Token);

            Console.WriteLine("Periodic refresh started.");
            Pause();
        }

        private void StopPeriodicRefresh()
        {
            if (_cts == null)
            {
                Console.WriteLine("No periodic refresh running.");
                Pause();
                return;
            }

            _cts.Cancel();
            _periodicTask?.Wait(2000);

            _cts = null;
            _periodicTask = null;

            Console.WriteLine("Periodic refresh stopped.");
            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
