using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading___Concurrency_in_C_
{
    internal class threadpooling
    {
        public static void Run()
        {
            Console.WriteLine("Starting background jobs using ThreadPooling");
            for (int i = 1; i <= 10; i++) 
            {
                int taskID = i;
                ThreadPool.QueueUserWorkItem(BackgroundTask, taskID);
            }
            Console.WriteLine("All tasks queued. \n");
            Thread.Sleep(5000);
            Console.WriteLine("\nAll background jobs completed.");
        }
        static void BackgroundTask(object? id)
        {
            Console.WriteLine($"Task {id} started on thread {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine($"Task {id} completed on thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
