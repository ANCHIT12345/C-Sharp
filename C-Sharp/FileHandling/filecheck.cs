using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandling
{
    class filecheck
    {
        public static void Run()
        {
            Console.WriteLine("File Check Program");
            Console.WriteLine("Choose an Option:");
            Console.WriteLine("1. Add new user");
            Console.WriteLine("2. View All Users");
            Console.WriteLine("Enter your option (1 or 2):");
            int option = Convert.ToInt32(Console.ReadLine());

            string filePath = "users.txt";

            if (option == 1)
            {
                Console.WriteLine("Enter username to add:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter email to add:");
                string email = Console.ReadLine();
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File Not Found - Creating a new one....");
                }
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{username},{email}");
                }
                Console.WriteLine("User added successfully.");
            }
            else if (option == 2)
            {
                if (File.Exists(filePath))
                {
                    Console.WriteLine("List of Users:");
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length == 2)
                            {
                                Console.WriteLine($"Username: {parts[0]}, Email: {parts[1]}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No users found. The file does not exist.");
                }
            }
            else
            {
                Console.WriteLine("Invalid option selected.");
            }
        }
    }
}
