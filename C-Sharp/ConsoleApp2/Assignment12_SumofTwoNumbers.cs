using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp12
{
    internal class Assignment12_SumofTwoNumbers
    {
        int sum(int a, int b) { 
            int c = a + b;
            return c;
        }
        public static void Run()
        {
            Console.WriteLine("Enter first number: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter second number: ");
            int b = Convert.ToInt32(Console.ReadLine());
            Assignment12_SumofTwoNumbers obj = new Assignment12_SumofTwoNumbers();
            Console.WriteLine("The sum of two numbers is: " + obj.sum(a, b));
            Console.ReadKey();
        }
    }
}
