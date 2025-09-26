using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    abstract class Shape
    {
        public string Color { get; set; }
        public Shape(string color)
        {
            Color = color;
        }
        public abstract void Draw();
    }
    interface IResizable
    {
        void Resize(double factor);
    }
    class Circle : Shape, IResizable
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
        public void Resize(double factor)
        {
            Radius *= factor;
            Console.WriteLine($"Resized circle to new radius {Radius}");
        }
    }
    class Rectangle : Shape, IResizable
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public Rectangle(string color, double length, double width) : base(color)
        {
            Length = length;
            Width = width;
        }
        public override void Draw()
        {
            Console.WriteLine($"Drawing a {Color} rectangle with length {Length} and width {Width}");
        }
        public void Resize(double factor)
        {
            Length *= factor;
            Width *= factor;
            Console.WriteLine($"Resized rectangle to new length {Length} and width {Width}");
        }
    }
}
