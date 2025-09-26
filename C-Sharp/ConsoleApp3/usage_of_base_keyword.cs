using MyApp3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class usage_of_base_keyword
    {
        public string Name { get; set; }
        public usage_of_base_keyword(string name)
        {
            Name = name;
            Console.WriteLine($"Person Constructor called {Name}");
        }
    }
    class Employee : usage_of_base_keyword
    {
        public decimal Salary { get; set; }
        public Employee(string name, decimal Salary) : base(name)
        {
            Salary = Salary;
            Console.WriteLine($"Employee Constructor called {Salary}");
        }
    }
    //class Manager : Employee
    //{
    //    public decimal bonus { get; set; }
    //    public Manager(string name, decimal Salary, string department) : base(name, Salary)
    //    {
    //        bonus = bonus;
    //        Console.WriteLine($"Manager Constructor called {department}");
    //    }
    //}
}
