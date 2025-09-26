using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class base_class_Vehicle_with_Start
    {
        public virtual void Start()
        {
            Console.WriteLine("Vehicle is starting");
        }
    }
    class Car : base_class_Vehicle_with_Start
    {
        public override void Start()
        {
            Console.WriteLine("Car is starting with a roar!");
        }
    }
    class Bike : base_class_Vehicle_with_Start
    {
        public override void Start()
        {
            Console.WriteLine("Bike is starting with a vroom!");
        }
    }
    class program
    {
        public static void Run() 
        {
            base_class_Vehicle_with_Start myVehicle;
            myVehicle = new Car();
            myVehicle.Start();
            myVehicle = new Bike();
            myVehicle.Start();
        }
    }
}
