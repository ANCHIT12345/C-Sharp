using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp16
{
    internal class Program16
    {
        public static void Run()
        {
            Dictionary<int, string> books = new Dictionary<int, string>();
            books.Add(1, "The Great Gatsby");
            books.Add(2, "To Kill a Mockingbird");
            books.Add(3, "1984");
            books.Add(4, "Pride and Prejudice");
            books.Add(5, "The Catcher in the Rye");
            books.Add(6, "The Hobbit");
            books.Add(7, "M");
            books.Add(8, "War and Peace");
            books.Add(9, "Ulysses");
            books.Add(10, "The Odyssey");
            books.Add(11, "Crime and Punishment");
            books.Add(12, "The Brothers Karamazov");    
            books.Add(13, "Brave New World");
            books.Add(14, "The Divine Comedy");
            books.Add(15, "The Iliad");
            books.Add(16, "One Hundred Years of Solitude");
            books.Add(17, "The Sound and the Fury");

            for (int i = 0; i <= 9; i++)
            {
                int key = books.Keys.ElementAt(i);
                string value = books[key];
                Console.WriteLine($"Key: {key}, Value: {value}");
            }
            
            Console.WriteLine("\n");
            Console.WriteLine("Using foreach loop with continue and break:");

            foreach (var book in books)
            {
                if(book.Key == 5) continue;
                if(book.Key == 8) break;
                Console.WriteLine($"Key: {book.Key}, Value: {book.Value}");
            }


        }
    }
}
