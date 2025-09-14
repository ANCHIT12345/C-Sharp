using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp20
{
    internal class Assignment20_MiniMenuwithMethods
    {
        static List<string> items = new List<string>();
        static void AddBook()
        {
            Console.Write("Enter book name to add: ");
            string bookName = Console.ReadLine();
            items.Add(bookName);
            Console.WriteLine($"Book '{bookName}' added.");
        }
        static void ViewBooks()
        {
            Console.WriteLine("Books in the list:");
            if (items.Count == 0)
            {
                Console.WriteLine("No books available.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item);
                }
            }
        }
        static void SearchBook()
        {
            Console.Write("Enter book name to search: ");
            string bookName = Console.ReadLine();
            if (items.Contains(bookName))
            {
                Console.WriteLine($"Book '{bookName}' found.");
            }
            else
            {
                Console.WriteLine($"Book '{bookName}' not found.");
            }
        }
        public static void Run()
        {
            int choice;
            do
            {
                Console.WriteLine("\nMini Menu:");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. Search Book");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("⚠️ Invalid input! Please enter a number.\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddBook();
                        break;
                    case 2:
                        ViewBooks();
                        break;
                    case 3:
                        Console.Write("Enter book title to search: ");
                        string title = Console.ReadLine();
                        SearchBook();
                        break;
                    case 4:
                        Console.WriteLine("👋 Exiting program...");
                        break;
                    default:
                        Console.WriteLine("⚠️ Invalid choice! Try again.\n");
                        break;
                }

            } while (choice != 4);
        }
    }
}
