using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp12
{
    internal class Program12
    {
        public static void Run()
        {
            int booksIssued = 0;
            const int maxBooks = 5;
            while (booksIssued < maxBooks)
            {
                Console.WriteLine("Book issued. Total books issued: " + (++booksIssued));
            }
            Console.WriteLine("Maximum books issued.");
            Console.ReadKey();
        }
    }
}
