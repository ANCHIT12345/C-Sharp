using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp17
{
    internal class Program17
    {
        int calculateFine(int days)
        {
            int fine = 0;
            if (days == 0) return fine;
            else
            {
                fine = days * 2;
            }
            return fine;
        }
        void displayFine(int fine)
        {
            Console.WriteLine("The fine for the book is: " + fine);
        }
        public static void Run()
        {
            Console.WriteLine("Enter the days the books was submited late:");
            int days = Convert.ToInt32(Console.ReadLine());
            Program17 program = new Program17();
            int fine = program.calculateFine(days);
            program.displayFine(fine);
        }
    }
}
