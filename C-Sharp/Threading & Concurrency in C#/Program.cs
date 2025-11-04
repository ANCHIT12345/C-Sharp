using System;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Threading___Concurrency_in_C_;

namespace threading
{
    class Program
    {
        static readonly object fileLock = new object();
        public static void Main(string[] args)
        {
            string filepath = "Log.txt";
            File.WriteAllText(filepath, "");
            //for (int i = 1; i <= 5; i++)                            // Unsafe file writing without locks
            //{
            //    int threadNum = 1;
            //    new Thread (()=> ParalelFileWriting(filepath, threadNum)).Start();
            //}
            //for (int i = 1; i <= 5; i++)                            // Safe file writing with locks
            //{
            //    int threadNum = i;
            //    new Thread(() => WriteToFileSafe(filepath, threadNum)).Start();
            //}
            //Console.WriteLine("Main thread finished.");
            //banking.Run();
            //TicketBooking.Run();
            //printqueue.Run();
            threadpooling.Run();
        }
        static void ParalelFileWriting(string path, int threadNum)
        {
            for (int i = 1; i <= 5; i++)
            {
                string message = $"Thread {threadNum} writing line {i}\n";
                File.AppendAllText(path, message);
                Thread.Sleep(10);
            }
        }
        static void WriteToFileSafe(string path, int threadNum)
        {
            for (int i = 1; i <= 5; i++)
            {
                lock (fileLock)
                {
                    string message = $"Thread {threadNum} writing line {i}\n";
                    File.AppendAllText(path, message);
                }
                Thread.Sleep(10);
            }
        }
    }
}