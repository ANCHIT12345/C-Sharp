using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyApp6
{
    internal class Assignment06_PrintJobs
    {
        public static void Run()
        {
            Queue<string> printQueue = new Queue<string>();
            printQueue.Enqueue("Job-1: Student Report");
            printQueue.Enqueue("Job-2: Book List");
            printQueue.Enqueue("Job-3: Product Inventory");
            printQueue.Enqueue("Job-4: Ticket Summary");
            printQueue.Enqueue("Job-5: Employee Records");
            printQueue.Enqueue("Job-6: Financial Report");
            Console.WriteLine("Initial Print Queue:");
            while (printQueue.Count > 0)
            {
                string job = printQueue.Dequeue();
                Console.WriteLine($"Processing {job}");
            }
            Console.WriteLine("All print jobs processed.");
        }
    }
}
