using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp16
{
    internal class Assignment16_MethodOverloading
    {
        static int Area(int side)
        {
            return side * side;
        }
        static int Area(int length, int breadth)
        {
            return length * breadth;
        }
        static double Area(double radius)
        {
            return Math.PI * radius * radius;
        }
        public static void Run()
        {
            Console.WriteLine("Area of Square with side 4: " + Area(4));
            Console.WriteLine("Area of Rectangle with length 4 and breadth 5: " + Area(4, 5));
            Console.WriteLine("Area of Circle with radius 3.5: " + Area(3.5));
        }
    }
}
