using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp17
{
    internal class Assignment17_RecursionFactorial
    {
        static int Factorial(int n)
        {
            if (n == 0)
                return 1;
            else
                return n * Factorial(n - 1);
        }
        public static void Run()
        { 
            Console.WriteLine("Enter a number to calculate its factorial:");
            int number = Convert.ToInt32(Console.ReadLine());
            int result = Factorial(number);
            Console.WriteLine($"Factorial of {number} is {result}");
        }
    }
}
