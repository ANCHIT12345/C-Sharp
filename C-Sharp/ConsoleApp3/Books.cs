using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp1
{
    internal class Books
    {
        string Title;
        string Author;
        double price;

        public Books(string title, string author, double price)
        {
            Title = title;
            Author = author;
            this.price = price;
        }
        public void Display()
        {
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Author: " + Author);
            Console.WriteLine("Price: " + price);
        }
    }
}
