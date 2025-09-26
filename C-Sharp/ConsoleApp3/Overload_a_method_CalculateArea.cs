using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Overload_a_method_CalculateArea
    {
        public double CalculateArea(double side)
        {
            return side * side;
        }
        public double CalculateArea(double length, double width)
        {
            return length * width;
        }
        public double CalculateArea(double radius, bool isCircle)
        {
            return Math.PI * radius * radius;
        }
    }
}
