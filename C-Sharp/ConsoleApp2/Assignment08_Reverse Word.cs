using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp8
{
    internal class Assignment08_Reverse_Word
    {
        public static void Run()
        {
            Console.WriteLine("Enter a word to reverse:");
            string input = Console.ReadLine();
            Stack<char> charStack = new Stack<char>();
            foreach (char c in input)
            {
                charStack.Push(c);
            }
            string reversed = "";
            while (charStack.Count > 0)
            {
                reversed += charStack.Pop();
            }
            Console.WriteLine($"Reversed word: {reversed}");
        }
    }
}
