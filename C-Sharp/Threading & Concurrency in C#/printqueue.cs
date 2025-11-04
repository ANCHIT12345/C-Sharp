using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading___Concurrency_in_C_
{
    class printqueue1
    {
        private static SemaphoreSlim semaphore = new SemaphoreSlim(3);
        public void PrintDocuments(string userName)
        {
            Console.WriteLine($"{userName} is waiting to print");
            semaphore.Wait();
            Console.WriteLine($"{userName} got access to a printer!");
            Thread.Sleep(2000);
            Console.WriteLine($"{userName} Finished printing");
            semaphore.Release();
        }
    }
    class printqueue
    {
        public static void Run()
        {
            printqueue1 printQueue = new printqueue1();
            for (int i = 1; i <= 10; i++)
            {
                string userName = $"User {i}";
                new Thread(() => printQueue.PrintDocuments($"User {userName}")).Start();
            }
            Console.ReadLine();
        }
    }
}
