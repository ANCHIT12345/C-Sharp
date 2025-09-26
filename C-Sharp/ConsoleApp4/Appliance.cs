using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    abstract class Appliance
    {
        public string Brand { get; set; }
        public Appliance(string brand)
        {
            Brand = brand;
        }
        public abstract void TurnOn();
    }
    class WashingMachine : Appliance
    {
        public WashingMachine(string brand) : base(brand) { }
        public override void TurnOn()
        {
            Console.WriteLine($"{Brand} Washing Machine is now ON.");
        }
    }
    class Refrigerator : Appliance
    {
        public Refrigerator(string brand) : base(brand) { }
        public override void TurnOn()
        {
            Console.WriteLine($"{Brand} Refrigerator is now ON.");
        }
    }
}
