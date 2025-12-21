using LeaderBoard.Models;
using LeaderBoard.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderBoard.Presentation
{
    public class PlayerModule
    {
        public static void ManagePlayers()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Players ===");
                Console.WriteLine("1. List Players");
                Console.WriteLine("2. View Player Details");
                Console.WriteLine("3. Create Player");
                Console.WriteLine("4. Update Player");
                Console.WriteLine("5. Delete Player");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Enter option: ");
                var choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            {
                                var ps = new PlayerService();
                                var pm = new PlayerModule(ps);
                                pm.ListPlayer();
                                break;
                            }
                        case "2":
                            {
                                var ps = new PlayerService();
                                var pm = new PlayerModule(ps);
                                pm.ViewPlayerDetails();
                                break;
                            }
                        case "3":
                            {
                                var ps = new PlayerService();
                                var pm = new PlayerModule(ps);
                                pm.CreatePlayer();
                                break;
                            }
                        case "4":
                            {
                                var ps = new PlayerService();
                                var pm = new PlayerModule(ps);
                                pm.UpdatePlayer();
                                break;
                            }
                        case "5":
                            {
                                var ps = new PlayerService();
                                var pm = new PlayerModule(ps);
                                pm.DeletePlayer();
                                break;
                            }
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Press any key to try again...");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
        private readonly IPlayerService _playerService;
        private const int PageSize = 20;
        public PlayerModule(IPlayerService playerService)
        {
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
        }
        public void ListPlayer()
        {
            var all = _playerService.GetAllPlayers();
            if (all == null || all.Count == 0)
            {
                Console.WriteLine("No players found.");
                Pause();
                return;
            }
            int page = 0;
            int pageCount = (int)Math.Ceiling(all.Count / (double)PageSize);
            while (true)
            {
                Console.Clear();
                var pageItems = all.Skip(page * PageSize).Take(PageSize).ToList();
                Console.WriteLine($"Players (page {page + 1}/{Math.Max(1, pageCount)}):");
                Console.WriteLine("ID\tName\t\tEmail");
                foreach (var p in pageItems)
                {
                    Console.WriteLine($"{p.UserID}\t{Truncate(p.UserName, 20),-20}\t{Truncate(p.Email, 30)}");
                }
                Console.WriteLine();
                Console.WriteLine("[N]ext page | [P]revious page | [B]ack");
                var nav = Console.ReadLine()?.Trim().ToLowerInvariant();
                if (nav == "n" && (page + 1) < pageCount) page++;
                else if (nav == "p" && page > 0) page--;
                else break;
            }
        }
        public void SreachPlayer()
        {
            Console.Write("Enter search term (name/email): ");
            var term = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(term))
            {
                Console.WriteLine("Empty search.");
                Pause();
                return;
            }

            var found = _playerService.GetAllPlayers()
                        .Where(u => (u.UserName ?? "").IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0
                                 || (u.Email ?? "").IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToList();

            if (found.Count == 0) Console.WriteLine("No matches.");
            else
            {
                Console.WriteLine("ID | Name | Email");
                foreach (var p in found) Console.WriteLine($"{p.UserID} | {p.UserName} | {p.Email}");
            }
            Pause();
        }
        public void ViewPlayerDetails()
        {
            Console.Write("Enter Player ID: ");
            if (!int.TryParse(Console.ReadLine(), out int vid))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var user = _playerService.GetPlayerById(vid);
            if (user == null)
            {
                Console.WriteLine("Player not found.");
                Pause();
                return;
            }

            DisplayPlayerDetails(user);
            Pause();
        }
        public void CreatePlayer()
        {
            var newUser = new User();
            Console.Write("Name: ");
            newUser.UserName = ReadNonEmpty("Name");
            Console.Write("Email: ");
            newUser.Email = ReadValidEmail();
            Console.Write("Phone (optional): ");
            newUser.PhoneNo = Console.ReadLine();
            Console.Write("UserType ID (optional): ");
            newUser.UtID = Console.ReadLine();

            try
            {
                int newId = _playerService.CreatePlayer(newUser);
                Console.WriteLine(newId > 0 ? $"Player created with ID {newId}" : "Create failed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create failed: {ex.Message}");
            }
            Pause();
        }
        public void UpdatePlayer()
        {
            Console.Write("Enter Player ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int uid))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var existing = _playerService.GetPlayerById(uid);
            if (existing == null)
            {
                Console.WriteLine("Player not found.");
                Pause();
                return;
            }

            Console.WriteLine("Leave blank to keep current value.");
            Console.Write($"Name ({existing.UserName}): ");
            var nn = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(nn)) existing.UserName = nn;
            Console.Write($"Email ({existing.Email}): ");
            var ne = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(ne)) existing.Email = ne;
            Console.Write($"Phone ({existing.PhoneNo}): ");
            var np = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(np)) existing.PhoneNo = np;
            Console.Write($"UserType ID ({existing.UtID}): ");
            var nut = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(nut)) existing.UtID = nut;

            try
            {
                var ok = _playerService.UpdatePlayer(existing);
                Console.WriteLine(ok ? "Updated successfully." : "Update failed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update error: {ex.Message}");
            }
            Pause();
        }

        public void DeletePlayer()
        {
            Console.Write("Enter Player ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int did))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Console.Write("Confirm delete (y/n): ");
            var c = Console.ReadLine();
            if (c?.ToLowerInvariant() == "y")
            {
                try
                {
                    var delOk = _playerService.DeletePlayer(did);
                    Console.WriteLine(delOk ? "Deleted." : "Delete failed. Check constraints.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Delete error: {ex.Message}");
                }
            }
            else Console.WriteLine("Delete cancelled.");

            Pause();
        }
        private static void DisplayPlayerDetails(User user)
        {
            Console.WriteLine("----- Player Details -----");
            Console.WriteLine($"ID: {user.UserID}");
            Console.WriteLine($"Name: {user.UserName}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Phone: {user.PhoneNo}");
            Console.WriteLine($"UserType: {user.UtID}");
        }

        private static string Truncate(string input, int length)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return input.Length <= length ? input : input.Substring(0, length - 3) + "...";
        }

        private static string ReadNonEmpty(string fieldName)
        {
            while (true)
            {
                var t = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(t)) return t;
                Console.Write($"{fieldName} is required. Please enter: ");
            }
        }

        private static string ReadValidEmail()
        {
            var validator = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            while (true)
            {
                var e = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(e)) { Console.Write("Email is required. Enter: "); continue; }
                if (!validator.IsValid(e)) { Console.Write("Invalid email. Enter a valid email: "); continue; }
                return e;
            }
        }
    }
}
