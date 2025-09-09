using System;
namespace MyApp2
{
  class Program2
  {
        const int Max_Books_Allowed = 5;
        public static void Run()
        {
            Console.WriteLine("Enter the number of books borrowed by a student:");
            if (!int.TryParse(Console.ReadLine(), out int numberOfBooks))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            if (numberOfBooks > Max_Books_Allowed)
            {
                Console.WriteLine("Error: A student cannot borrow more than " + Max_Books_Allowed + " books.");
            }
            else
            {
                Console.WriteLine("Books borrowed successfully.");
            }
            Console.ReadKey();
        }
  }
}