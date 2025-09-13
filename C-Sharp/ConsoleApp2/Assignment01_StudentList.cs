using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp1
{
    internal class Assignment01_StudentList
    {
        public static void Run()
        {
            List<string> strings = new List<string> { "Alice", "Bob", "Charlie", "Diana" };
            foreach (var str in strings)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine("\n");
            Console.WriteLine("Enter more names you want to add seperated with spaces");
            string input = Console.ReadLine();
            for (int i = 0; i < input.Split(' ').Length; i++)
            {
                strings.Add(input.Split(' ')[i]);
            }
            Console.WriteLine("\n");
            Console.WriteLine("Updated List:");
            foreach (var str in strings)
            {
                Console.WriteLine(str);
            }
        }
    }
}
