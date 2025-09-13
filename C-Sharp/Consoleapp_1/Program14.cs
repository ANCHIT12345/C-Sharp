using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp14
{
    internal class Program14
    {
        public static void Run()
        {
            string[] books = { "The Great Gatsby", "1984", "To Kill a Mockingbird", "Pride and Prejudice", "C#", "SQL", "React", "Azure" };
            foreach (var book in books) 
            {
                Console.WriteLine(book);
            }
        }
    }
}
