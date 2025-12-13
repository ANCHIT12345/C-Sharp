using System;
using System.Threading;
using System.Threading.Tasks;
using Leaderboard.Services;


namespace Leaderboard.UI
{
    public class LeaderboardModule
    {
        private readonly LeaderboardService _leaderboardService;
        private readonly RatingService _ratingService;

        public LeaderboardModule(LeaderboardService leaderboardService, RatingService ratingService)
        {
            _leaderboardService = leaderboardService ?? throw new ArgumentNullException(nameof(leaderboardService));
            _ratingService = ratingService ?? throw new ArgumentNullException(nameof(ratingService));
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Leaderboard ===");
                Console.WriteLine("1. Generate & Display Leaderboard (One-off)");
                Console.WriteLine("2. Export Leaderboard to CSV");
                Console.WriteLine("3. Generate & Update Ratings for Contest");
                Console.WriteLine("4. Periodic Auto-refresh (start)");
                Console.WriteLine("5. Stop Auto-refresh");
                Console.WriteLine("6. Back");
                Console.Write("Choice: ");
                var c = Console.ReadLine();
                switch (c)
                {
                    case "1": GenerateAndDisplay(); break;
                    case "2": Export(); break;
                    case "3": GenerateAndUpdateRatings(); break;
                    case "4": StartPeriodicRefresh(); break;
                    case "5": StopPeriodicRefresh(); break;
                    case "6": return;
                    default: Console.WriteLine("Invalid"); Pause(); break;
                }
            }
        }

        private void GenerateAndDisplay()
        {
            Console.Write("Contest ID: ");
            if (!int.TryParse(Console.ReadLine(), out int cid)) { Console.WriteLine("Invalid"); Pause(); return; }
            _leaderboardService.DisplayLeaderboard(cid);
            Pause();
        }

        private void Export()
        {
            Console.Write("Contest ID: ");
            if (!int.TryParse(Console.ReadLine(), out int cid)) { Console.WriteLine("Invalid"); Pause(); return; }
            Console.Write("Directory to save CSV (default ./exports): ");
            var dir = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(dir)) dir = "./exports";
            var path = _leaderboardService.ExportLeaderboardToCsv(cid, dir);
            Console.WriteLine($"Exported to: {path}");
            Pause();
        }

        private void GenerateAndUpdateRatings()
        {
            Console.Write("Contest ID: ");
            if (!int.TryParse(Console.ReadLine(), out int cid)) { Console.WriteLine("Invalid"); Pause(); return; }
            var rows = _leaderboardService.GenerateLeaderboard(cid);
            _ratingService.UpdateRatingsForContest(rows);
            Console.WriteLine("Ratings updated for participating players.");
            Pause();
        }
        private CancellationTokenSource _cts;
        private Task _periodicTask;

        private void StartPeriodicRefresh()
        {
            Console.Write("Enter interval seconds (default 10): ");
            var s = Console.ReadLine();
            if (!int.TryParse(s, out int interval)) interval = 10;
            if (_cts != null) { Console.WriteLine("Already running."); Pause(); return; }
            _cts = new CancellationTokenSource();
            _periodicTask = _leaderboardService.PeriodicRefreshAsync(interval, _cts.Token);
            Console.WriteLine("Periodic refresh started.");
            Pause();
        }

        private void StopPeriodicRefresh()
        {
            if (_cts == null) { Console.WriteLine("Not running."); Pause(); return; }
            _cts.Cancel();
            _periodicTask?.Wait(2000);
            _cts = null;
            _periodicTask = null;
            Console.WriteLine("Stopped.");
            Pause();
        }

        private static void Pause() { Console.WriteLine("Press Enter..."); Console.ReadLine(); }
    }
}
