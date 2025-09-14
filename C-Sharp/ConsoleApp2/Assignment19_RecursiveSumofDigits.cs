using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp19
{
    internal class Assignment19_RecursiveSumofDigits
    {
        static int SumOfDigits(int n)
        {
            if (n == 0)
                return 0;
            return (n % 10 + SumOfDigits(n / 10));
        }
        public static void Run()
        { 
            Console.WriteLine("Enter a number:");
            int number = Convert.ToInt32(Console.ReadLine());
            int result = SumOfDigits(number);
            Console.WriteLine($"The sum of digits in {number} is: {result}");
        }
    }
}
