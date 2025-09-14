using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp13
{
    internal class Assignment13_PassbyValue
    {
        static void ChangeValue(int num)
        {
            num = 50; // This change will not affect the original variable
            Console.WriteLine("Inside ChangeValue method: " + num);
        }
        public static void Run()
        { 
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Before ChangeValue method: " + x);
            ChangeValue(x);
            Console.WriteLine("After ChangeValue method: " + x);
        }
    }
}
