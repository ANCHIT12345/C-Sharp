using LeaderBoard.Models;
using LeaderboardApp.Models;
using LeaderboardApp.Services;
using System;

namespace LeaderboardApp.UI
{
    public class ContestModule
    {
        private readonly ContestService _contestService;
        public ContestModule(ContestService contestService) { _contestService = contestService; }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Contest Management ===");
                Console.WriteLine("1. List Contests");
                Console.WriteLine("2. Create Contest");
                Console.WriteLine("3. Update Contest");
                Console.WriteLine("4. Delete Contest");
                Console.WriteLine("5. List Active Contests");
                Console.WriteLine("6. Back");
                Console.Write("Choice: ");
                var c = Console.ReadLine();
                try
                {
                    switch (c)
                    {
                        case "1": ListAll(); break;
                        case "2": Create(); break;
                        case "3": Update(); break;
                        case "4": Delete(); break;
                        case "5": ListActive(); break;
                        case "6": return;
                        default: Console.WriteLine("Invalid"); Pause(); break;
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); Pause(); }
            }
        }

        private void ListAll()
        {
            var list = _contestService.GetAllContests();
            if (list == null || list.Count == 0) { Console.WriteLine("No contests."); Pause(); return; }
            Console.WriteLine("ID | StartDate - EndDate | Matches");
            foreach (var co in list)
            {
                Console.WriteLine($"{co.ContestId} | {co.ContestStartDate?.ToShortDateString()} - {co.ContestEndDate?.ToShortDateString()} | {co.TotalNumberOfMatches}");
            }
            Pause();
        }

        private void Create()
        {
            var c = new Contest();
            Console.Write("Start Date (yyyy-MM-dd): "); if (DateTime.TryParse(Console.ReadLine(), out var s)) c.ContestStartDate = s;
            Console.Write("End Date (yyyy-MM-dd): "); if (DateTime.TryParse(Console.ReadLine(), out var e)) c.ContestEndDate = e;
            Console.Write("Total Matches (int): "); c.TotalNumberOfMatches = int.TryParse(Console.ReadLine(), out int t) ? t : 0;
            Console.Write("Contest Type ID (CtID): "); c.CtID = Console.ReadLine();
            var id = _contestService.CreateContest(c);
            Console.WriteLine($"Created Contest {id}");
            Pause();
        }

        private void Update()
        {
            Console.Write("Contest ID to update: "); if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid"); Pause(); return; }
            var existing = _contestService.GetContest(id);
            if (existing == null) { Console.WriteLine("Not found"); Pause(); return; }
            Console.Write($"Start Date ({existing.ContestStartDate?.ToString("yyyy-MM-dd")}): ");
            var s = Console.ReadLine(); if (DateTime.TryParse(s, out var sd)) existing.ContestStartDate = sd;
            Console.Write($"End Date ({existing.ContestEndDate?.ToString("yyyy-MM-dd")}): ");
            var e = Console.ReadLine(); if (DateTime.TryParse(e, out var ed)) existing.ContestEndDate = ed;
            Console.Write($"Total Matches ({existing.TotalNumberOfMatches}): ");
            var t = Console.ReadLine(); if (int.TryParse(t, out int tt)) existing.TotalNumberOfMatches = tt;
            var ok = _contestService.UpdateContest(existing);
            Console.WriteLine(ok ? "Updated." : "Update failed.");
            Pause();
        }

        private void Delete()
        {
            Console.Write("Contest ID to delete: "); if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid"); Pause(); return; }
            Console.Write("Confirm (y/n): "); if (Console.ReadLine()?.ToLowerInvariant() != "y") { Console.WriteLine("Cancelled"); Pause(); return; }
            var ok = _contestService.DeleteContest(id);
            Console.WriteLine(ok ? "Deleted." : "Delete failed (check constraints).");
            Pause();
        }

        private void ListActive()
        {
            var now = DateTime.Now;
            var list = _contestService.GetActiveContests(now);
            if (list == null || list.Count == 0) { Console.WriteLine("No active contests now."); Pause(); return; }
            Console.WriteLine("Active Contests:");
            foreach (var co in list) Console.WriteLine($"{co.ContestId} | {co.ContestStartDate} - {co.ContestEndDate}");
            Pause();
        }

        private static void Pause() { Console.WriteLine("Press Enter..."); Console.ReadLine(); }
    }
}
