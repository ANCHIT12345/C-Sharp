using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    abstract class Vehicle
    {
        public string Brand { get; set; }
        public Vehicle(string brand)
        {
            Brand = brand;
        }
        public abstract void Drive();
        public void FuleType()
        {
            Console.WriteLine($"{Brand} uses petrol as fuel");
        }
    }
    class car : Vehicle
    {
        public car(string brand) : base(brand) { }
        public override void Drive()
        {
            Console.WriteLine($"{Brand} is driving");
        }
    }
    class Bike : Vehicle
    {
        public Bike(string brand) : base(brand) { }
        public override void Drive()
        {
            Console.WriteLine($"{Brand} is driving");
        }
    }
}
