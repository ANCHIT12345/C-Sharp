using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp18
{
    internal class Assignment18_RecursionFibonacci
    {
        static int Fibonacci(int x)
        {
            if (x <= 0)
                return 0;
            else if (x == 1)
                return 1;
            else
                return Fibonacci(x - 1) + Fibonacci(x - 2);
        }
        public static void Run()
        { 
            Console.WriteLine("Enter a number to calculate its Fibonacci:");
            int number = Convert.ToInt32(Console.ReadLine());
            int result = Fibonacci(number);
            Console.WriteLine($"Fibonacci of {number} is {result}");
        }
    }
}
