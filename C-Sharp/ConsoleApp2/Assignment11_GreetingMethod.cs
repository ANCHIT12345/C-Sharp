using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp11
{
    internal class Assignment11_GreetingMethod
    {
        static void greetUser(string name)
        {
            Console.WriteLine($"Hello, {name}! Welcome to the program.");
        }
        public static void Run()
        {
            greetUser("Alice");
            greetUser("Bob");
            greetUser("Charlie");
        }
    }
}
