using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyApp15
{
    internal class Assignment15_PassbyReferenceout
    {
        static void GetStudent(out string name, out int age)
        {
            name = "John Doe";
            age = 20;
            Console.WriteLine($"Inside Method: Name = {name}, Age = {age}");
        }
        public static void Run()
        {
            string studentName;
            int studentAge;
            GetStudent(out studentName, out studentAge);
            Console.WriteLine($"Outside Method: Name = {studentName}, Age = {studentAge}");
        }
    }
}
