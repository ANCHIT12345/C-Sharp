using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyApp11
{
    internal class Program11
    {
        public static void Run() 
        {
            Dictionary<string, string> books = new Dictionary<string, string>();
            books.Add("Bk101", "The Great Book");
            books.Add("Bk102", "The Next Great Book");
            books.Add("Bk103", "The Last Great Book");
            books.Add("Bk104", "The Final Great Book");
            books.Add("Bk105", "The Ultimate Great Book");
            books.Add("Bk106", "The Supreme Great Book");
            books.Add("Bk107", "The Greatest Book");
            books.Add("Bk108", "The Most Great Book");
            books.Add("Bk109", "The Incomparable Great Book");
            books.Add("Bk110", "The Unmatched Great Book");
            books.Add("Bk111", "The Peerless Great Book");
            books.Add("Bk112", "The Nonpareil Great Book");
            books.Add("Bk113", "The Unequaled Great Book");
            books.Add("Bk114", "The Unrivaled Great Book");
            books.Add("Bk115", "The Matchless Great Book");
            Console.WriteLine("enter the number for how long you the list of books you want to be:");
            int size = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i <= size; i++) 
            {
                string key = books.Keys.ElementAt(i);
                string value = books[key];
                Console.WriteLine($"Key: {key}, Value: {value}");
            }
        }
    }
}
