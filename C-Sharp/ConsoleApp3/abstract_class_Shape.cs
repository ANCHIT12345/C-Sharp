using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    abstract class abstract_class_Shape(string color)
    {
        public string Color { get; set; } = color;

        public abstract void Draw();
    }
    class Circle : abstract_class_Shape
    {
        public double Radius { get; set; }
        public Circle(string color, double radius) : base(color)
        {
            Radius = radius;
        }
        public override void Draw()
        {
            Console.WriteLine($"Drawing a {Color} circle with radius {Radius}");
        }
    }
    class Rectangle : abstract_class_Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public Rectangle(string color, double width, double height) : base(color)
        {
            Width = width;
            Height = height;
        }
        public override void Draw()
        {
            Console.WriteLine($"Drawing a {Color} rectangle with width {Width} and height {Height}");
        }
    }
}
