using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp2
{
    internal class Assignment02_BookPriceList
    {
        public static void Run()
        { 
            List<double> book_price = new List<double>();
            book_price.Add(250.00);
            book_price.Add(300.00);
            book_price.Add(150.00);
            book_price.Add(400.00);
            book_price.Add(500.00);
            book_price.Add(350.00);
            book_price.Add(450.00);
            book_price.Add(600.00);
            book_price.Add(700.00);
            book_price.Add(800.00);
            double highest_price = book_price.Max();
            double lowest_price = book_price.Min();
            foreach (double price in book_price)
            {
                Console.WriteLine("Book Price: " + price);
            }
            Console.WriteLine("Highest Price: " + highest_price);
            Console.WriteLine("Lowest Price: " + lowest_price);
        }
    }
}
