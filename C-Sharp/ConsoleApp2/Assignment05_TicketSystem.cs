using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp5
{
    internal class Assignment05_TicketSystem
    {
        public static void Run()
        {
            Queue<string> StudentProcessingQueue = new Queue<string>();
            StudentProcessingQueue.Enqueue("Alice");
            StudentProcessingQueue.Enqueue("Bob");
            StudentProcessingQueue.Enqueue("Charlie");
            StudentProcessingQueue.Enqueue("Diana");
            StudentProcessingQueue.Enqueue("Ethan");
            Console.WriteLine("Initial Queue:");
            while (StudentProcessingQueue.Count > 0)
            {
                string currentStudent = StudentProcessingQueue.Dequeue();
                Console.WriteLine($"\nProcessing ticket for: {currentStudent}");
                Console.WriteLine($"Issued librrary card to {currentStudent}");
            }
            Console.WriteLine("\nAll tickets processed. Queue is empty.");
        }
    }
}
