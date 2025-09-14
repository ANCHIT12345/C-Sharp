using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp10 { 
    internal class Assignment10_foreachwithCollections
    {
        public static void Run()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            foreach (int number in numbers)
            {
                if (number % 2 == 0)
                {
                    Console.WriteLine($"{number} is even.");
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
