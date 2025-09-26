using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class multi_level_inheritance
    {
        public void Breath()
        {
            Console.WriteLine("Living Being: Breathing...");
        }
    }
    class Human : multi_level_inheritance
    {
        public string Name { get; set; }
        public Human(string name)
        {
            Name = name;
        }
        public void Speak()
        {
            Console.WriteLine($"{Name} is speaking.");
        }
    }
    class Teacher : Human
    {
        public string Subject { get; set; }
        public Teacher(string name, string subject) : base(name)
        {
            Subject = subject;
        }
        public void Teach()
        {
            Console.WriteLine($"{Name} is teaching {Subject}.");
        }
    }
}
