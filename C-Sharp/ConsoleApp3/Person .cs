using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp3
{
    internal class Person
    {
        public string Name;
        public int age;
        public Person(string name, int age)
        {
            Name = name;
            this.age = age;
        }
        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Age: {age}");
        }
    }
    class Teacher : Person 
    {
        public string Subject;

        public Teacher(string name, int age) : base(name, age)
        {
        }

        public Teacher(string name, int age, string subject) : base(name, age)
        {
            Subject = subject;
        }
        public new void Display()
        {
            base.Display();
            Console.WriteLine($"Subject: {Subject}");
        }
    }
}
