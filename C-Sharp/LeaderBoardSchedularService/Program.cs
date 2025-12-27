//Existing Application Change:
//1.Validation Missing in Game module
//2. Validation missing in Contest module
//3. Leaderboard module - add missing repo layer
//Scheduler Console App
//1. Calculate Global Leaderboard
//2. Calculate Active Contest Leaderboard
//Diagram
//1. Class Diagram
//2. Use Case diagram
//3. Activity diagram

using System;
using System.Threading;
using Leaderboard.Data;
using Leaderboard.Services;

namespace LeaderBoardSchedulerService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Leaderboard Scheduler Service";

            var db = new DatabaseHelper();
            var leaderboardRepo = new LeaderboardRepository(db);
            var scheduler = new LeaderboardSchedulerService(leaderboardRepo);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("     LEADERBOARD SCHEDULER SYSTEM    ");
                Console.WriteLine("====================================");
                Console.WriteLine("1. Run Leaderboard Once");
                Console.WriteLine("2. Run Scheduler (Auto Refresh)");
                Console.WriteLine("3. Exit");
                Console.Write("Select option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RunOnce(scheduler);
                        break;

                    case "2":
                        RunScheduler(scheduler);
                        break;

                    case "3":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private static void RunOnce(LeaderboardSchedulerService scheduler)
        {
            try
            {
                scheduler.RunOnce();
                Console.WriteLine("Leaderboard successfully generated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Pause();
        }

        private static void RunScheduler(LeaderboardSchedulerService scheduler)
        {
            Console.Write("Enter refresh interval (seconds): ");
            if (!int.TryParse(Console.ReadLine(), out int interval))
                interval = 10;

            var cts = new CancellationTokenSource();

            Console.WriteLine("Scheduler running...");
            Console.WriteLine("Press CTRL + C to stop.");

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
                Console.WriteLine("\nStopping scheduler...");
            };

            try
            {
                scheduler.RunPeriodicAsync(interval, cts.Token).Wait();
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Scheduler stopped.");
            }

            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress ENTER to continue...");
            Console.ReadLine();
        }
    }
}
