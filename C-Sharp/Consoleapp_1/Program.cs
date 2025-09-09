using System;
namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Delegate to Program2.Run in MyApp namespace to run the second example
            MyApp1.Program1.Run();
            MyApp2.Program2.Run();
            MyApp3.Program3.Run();
        }
    }
}