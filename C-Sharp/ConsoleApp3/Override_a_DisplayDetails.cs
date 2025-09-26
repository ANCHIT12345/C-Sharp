using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Override_a_DisplayDetails
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public Override_a_DisplayDetails(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
        }
        public virtual void DisplayDetails()
        {
            Console.WriteLine($"Name: {Name}, Salary: {Salary}");
        }
    }
    class Manager : Override_a_DisplayDetails
    {
        public decimal Bonus { get; set; }
        public Manager(string name, decimal salary, decimal bonus) : base(name, salary)
        {
            Bonus = bonus;
        }
        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Bonus: {Bonus}");
        }
    }
}
