using System;
namespace test
{
    internal class Program 
    {
        static void Main(string[] args)
        {
            // Read number of terms to print in the Fibonacci sequence
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 0)
            {
                Console.WriteLine("Enter a non-negative integer.");
                return;
            }

            if (n == 0)
                return; // no output

            if (n == 1)
            {
                Console.WriteLine("0");
                return;
            }

            long a = 0;
            long b = 1;
            Console.Write("0 1");
            for (int i = 3; i <= n; i++)
            {
                long c = a + b;
                Console.Write(" " + c);
                a = b;
                b = c;
            }
            Console.WriteLine();
        }
    }
}