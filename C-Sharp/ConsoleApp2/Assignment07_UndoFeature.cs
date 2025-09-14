using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp7
{
    internal class Assignment07_UndoFeature
    {
        public static void Run()
        {
            Stack<string> action = new Stack<string>();
            action.Push("Typed A");
            action.Push("Typed B");
            action.Push("Deleted C");

            Console.WriteLine("Undoing actions:\n");
            while (action.Count > 0)
            {
                Console.WriteLine($"Action undone: {action.Pop()}");
            }
            Console.WriteLine("\nAll actions Undone!");
        }
    }
}
