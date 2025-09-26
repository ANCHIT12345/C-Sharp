using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class base_class_Employee
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public base_class_Employee(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
        }
        public virtual void ShowDetails()
        {
            Console.WriteLine($"Name: {Name}, Salary: {Salary}");
        }

        internal object GetTotalSalary()
        {
            throw new NotImplementedException();
        }
    }
    class manager : base_class_Employee
    {
        public string Department { get; set; }
        public manager(string name, decimal salary, string department)
            : base(name, salary)
        {
            Department = department;
        }
        public override void ShowDetails()
        {
            base.ShowDetails();
            Console.WriteLine($"Department: {Department}");
        }
    }
}
