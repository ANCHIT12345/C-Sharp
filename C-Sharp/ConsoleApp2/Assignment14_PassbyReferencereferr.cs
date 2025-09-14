using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp14
{
    internal class Assignment14_PassbyReferencereferr
    {
        static int Square(ref int x)
        {
            x = x * x;
            return x;
        }
        public static void Run()
        {
            Console.WriteLine("Enter a number to find its square:");
            int num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Before calling Square method: " + num);
            Console.WriteLine("Calling Square method...");
            Square(ref num);
            Console.WriteLine("Inside Square method...");
            Console.WriteLine("After squaring: " + num);
        }
    }
}
