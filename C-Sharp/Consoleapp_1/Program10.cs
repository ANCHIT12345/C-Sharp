using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp10
{
    internal class Program10
    {
        public static void Run()
        {
            Console.WriteLine("Enter what you want to execute (Add book, Issue book, Return Book, Exit): ");
            string? command = Console.ReadLine();
            if (command == null)
            {
                Console.WriteLine("No command entered.");
                return;
            }
            switch (command)
            {
                case "Add book":
                    Console.WriteLine("Enter the ammount of books you want to add:");
                    string? input = Console.ReadLine();
                    if (int.TryParse(input, out int n))
                    {
                        Console.WriteLine($"{n} books added to the library.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid number entered.");
                    }
                    break;
                case "Issue book":
                    Console.WriteLine("Book Issued");
                    break;
                case "Return Book":
                    Console.WriteLine("Book Returned");
                    break;
                case "Exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }
        }
    }
}
