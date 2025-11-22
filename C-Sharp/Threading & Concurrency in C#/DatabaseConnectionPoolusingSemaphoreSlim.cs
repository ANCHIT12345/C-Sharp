using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading___Concurrency_in_C_
{
    internal class DatabaseConnectionPoolusingSemaphoreSlim
    {
        public static void Run()
        {
            main().GetAwaiter().GetResult();
        }
        public static async Task main()
        {
            Program dbPool = new Program();
            List<Task> tasks = new List<Task>();
            for (int i = 1; i <= 10; i++)
            {
                string user = $"User {i}";
                tasks.Add(dbPool.AccessDatabaseAsync(user));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("All users have completed their database operations.");
        }
    }
    class Program
    {
        private static SemaphoreSlim semaphore = new SemaphoreSlim(3);
        public async Task AccessDatabaseAsync(string user)
        {
            Console.WriteLine($"{user} is waiting to access the database.");
            await semaphore.WaitAsync();
            try
            {
                Console.WriteLine($"{user} has acquired a database connection.");
                await Task.Delay(2000);
                Console.WriteLine($"{user} has finished database operations.");
            }
            finally
            {
                semaphore.Release();
                Console.WriteLine($"{user} has released the database connection.");
            }
        }
    }
}
