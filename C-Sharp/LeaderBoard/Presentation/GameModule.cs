using LeaderBoard.Models;
using Leaderboard.Services;
using System;
using System.Linq;
using Leaderboard.Models;

namespace LeaderBoard.Presentation
{
    public class GameModule
    {
        private readonly GameService _gameService;
        public GameModule(GameService gameService)
        {
            _gameService = gameService;
        }

        public static void ManageGames()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Game Management ===");
                Console.WriteLine("1. List Games");
                Console.WriteLine("2. Import Games from JSON file");
                Console.WriteLine("3. Create Game");
                Console.WriteLine("4. Update Game");
                Console.WriteLine("5. Delete Game");
                Console.WriteLine("6. Back");
                Console.Write("Choice: ");
                var c = Console.ReadLine();
                try
                {
                    switch (c)
                    {
                        case "1":
                            {
                                var gm = new GameService();
                                var module = new GameModule(gm);
                                module.ListGames();
                                break;
                            }
                        case "2":
                            {
                                var gm = new GameService();
                                var module = new GameModule(gm);
                                module.ImportGames();
                                break;
                            }
                        case "3":
                            {
                                var gm = new GameService();
                                var module = new GameModule(gm);
                                module.CreateGame();
                                break;
                            }
                        case "4":
                            {
                                var gm = new GameService();
                                var module = new GameModule(gm);
                                module.UpdateGame();
                                break;
                            }
                        case "5":
                            {
                                var gm = new GameService();
                                var module = new GameModule(gm);
                                module.DeleteGame();
                                break;
                            }
                        case "6":
                            return;
                        default: 
                            Console.WriteLine("Invalid choice."); 
                            Pause();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Pause();
                }
            }
        }

        private void ListGames()
        {
            var all = _gameService.GetAllGames();
            if (all == null || all.Count == 0) { Console.WriteLine("No games."); Pause(); return; }
            Console.WriteLine("ID | Date | Start - End | Rounds");
            foreach (var g in all)
            {
                Console.WriteLine($"{g.GameId} | {g.GameHeldDate?.ToShortDateString()} | {g.GameStartTime} - {g.GameEndTime} | {g.GameRoundsHeld}");
            }
            Pause();
        }

        private void ImportGames()
        {
            Console.Write("Enter JSON file path: ");
            var path = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(path)) { Console.WriteLine("Path required."); Pause(); return; }
            try
            {
                int changed = _gameService.ImportGamesFromJson(path);
                Console.WriteLine($"Imported/Updated {changed} game records.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Import failed: {ex.Message}");
            }
            Pause();
        }

        private void CreateGame()
        {
            var g = new Game();
            Console.Write("Held Date (yyyy-MM-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out var d)) g.GameHeldDate = d;
            Console.Write("Start Time (HH:mm): "); g.GameStartTime = Console.ReadLine();
            Console.Write("End Time (HH:mm): "); g.GameEndTime = Console.ReadLine();
            Console.Write("Rounds held (int): "); g.GameRoundsHeld = int.TryParse(Console.ReadLine(), out int r) ? r : 0;
            Console.Write("Winner PlayerID (optional): "); g.GameWinner = int.TryParse(Console.ReadLine(), out int w) ? w : 0;
            Console.Write("MVP PlayerID (optional): "); g.GameMVP = int.TryParse(Console.ReadLine(), out int m) ? m : 0;
            Console.Write("RunnerUp PlayerID (optional): "); g.RunnerUp = int.TryParse(Console.ReadLine(), out int ru) ? ru : 0;
            Console.Write("BestTime (optional): "); g.BestTime = Console.ReadLine();
            Console.Write("ContestType ID (CtID) (optional): "); g.CtID = Console.ReadLine();
            Console.Write("Location ID (LtID) (optional int): "); g.LtID = int.TryParse(Console.ReadLine(), out int li) ? li : 0;

            try
            {
                var id = _gameService.CreateGame(g);
                Console.WriteLine(id > 0 ? $"Inserted game {id}" : "Insert failed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Pause();
        }

        private void UpdateGame()
        {
            Console.Write("Enter Game ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int gid)) { Console.WriteLine("Invalid"); Pause(); return; }
            var existing = _gameService.GetGame(gid);
            if (existing == null) { Console.WriteLine("Not found"); Pause(); return; }
            Console.WriteLine("Leave blank to keep existing value.");
            Console.Write($"Held Date ({existing.GameHeldDate?.ToString("yyyy-MM-dd")}): ");
            var d = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(d) && DateTime.TryParse(d, out var dd)) existing.GameHeldDate = dd;
            Console.Write($"Start Time ({existing.GameStartTime}): "); var st = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(st)) existing.GameStartTime = st;
            Console.Write($"End Time ({existing.GameEndTime}): "); var et = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(et)) existing.GameEndTime = et;
            Console.Write($"Rounds ({existing.GameRoundsHeld}): "); var r = Console.ReadLine(); if (int.TryParse(r, out int rr)) existing.GameRoundsHeld = rr;
            Console.Write($"Winner ({existing.GameWinner}): "); if (int.TryParse(Console.ReadLine(), out int w)) existing.GameWinner = w;
            try
            {
                var ok = _gameService.UpdateGame(existing);
                Console.WriteLine(ok ? "Updated." : "Update failed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update error: {ex.Message}");
            }
            Pause();
        }

        private void DeleteGame()
        {
            Console.Write("Enter Game ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int gid)) { Console.WriteLine("Invalid"); Pause(); return; }
            Console.Write("Confirm (y/n): "); if (Console.ReadLine()?.ToLowerInvariant() != "y") { Console.WriteLine("Cancelled"); Pause(); return; }
            try
            {
                var ok = _gameService.DeleteGame(gid);
                Console.WriteLine(ok ? "Deleted." : "Delete failed (check constraints).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete error: {ex.Message}");
            }
            Pause();
        }

        private static void Pause() { Console.WriteLine("Press Enter..."); Console.ReadLine(); }
    }
}
