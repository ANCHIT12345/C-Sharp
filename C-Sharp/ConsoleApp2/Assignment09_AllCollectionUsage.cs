using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp9
{
    internal class Assignment09_AllCollectionUsage
    {
        public static void Run()
        { 
            List<string> books = new List<string> { "Book A", "Book B", "Book C" };
            Console.WriteLine("Books List:");
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
            Dictionary<int, string> studentGrades = new Dictionary<int, string>
            {
                { 1, "A" },
                { 2, "B" },
                { 3, "C" }
            };
            foreach (var kvp in studentGrades)
            {
                Console.WriteLine($"Student ID: {kvp.Key}, Grade: {kvp.Value}");
            }
            Queue<string> printQueue = new Queue<string>();
            printQueue.Enqueue("Document1");
            printQueue.Enqueue("Document2");
            Console.WriteLine("Print Queue:");
            while (printQueue.Count > 0)
            {
                Console.WriteLine(printQueue.Dequeue());
            }
            Stack<string> undoStack = new Stack<string>();
            undoStack.Push("Action1");
            undoStack.Push("Action2");
            Console.WriteLine("Undo Stack:");
            while (undoStack.Count > 0)
            {
                Console.WriteLine(undoStack.Pop());
            }
        }
    }
}
