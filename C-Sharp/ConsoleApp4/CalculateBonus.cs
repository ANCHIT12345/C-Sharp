using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    abstract class CalculateBonus
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public CalculateBonus(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
        }
        public abstract decimal CalcBonus();
        public void DisplayBonus()
        {
            Console.WriteLine($"Employee: {Name}, Salary: {Salary}, Bonus: {CalcBonus()}");
        }
    }
    class Manager : CalculateBonus
    {
        public Manager(string name, decimal salary) : base(name, salary) { }
        public override decimal CalcBonus()
        {
            return Salary * 0.2m;
        }
    }
    class Developer : CalculateBonus
    {
        public Developer(string name, decimal salary) : base(name, salary) { }
        public override decimal CalcBonus()
        {
            return Salary * 0.1m;
        }
    }
}
