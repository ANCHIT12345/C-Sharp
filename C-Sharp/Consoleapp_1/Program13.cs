using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp13
{
    internal class Program13
    { 
      public static void Run()
        {
            Console.WriteLine("Do you want to contineu borrowing books (yes,no):");
            string? answer = Console.ReadLine();
            do
            {
                if (answer == "yes")
                {
                    Console.WriteLine("You can borrow books");
                }
                else if (answer == "no")
                {
                    Console.WriteLine("You cannot borrow books");
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter yes or no");
                }
                Console.WriteLine("Do you want to contineu borrowing books (yes,no):");
                answer = Console.ReadLine();
            } while (answer != "no");
        }
    }
}
