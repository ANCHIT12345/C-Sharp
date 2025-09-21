using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp4
{
    internal class method_overloading
    {
        public double Area(double length, double width)
        {
             return length * width;
        }
        public double Area(double radius)
        {
            return (int)(3.14 * radius * radius);
        }
    }
}
