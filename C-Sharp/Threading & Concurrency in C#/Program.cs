using System;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace threading
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("Hello from the new thread!");
            });
            thread.Start();
            thread.Join(); // Wait for the thread to finish
            Console.WriteLine("Main thread finished.");
        }
        static void ParalelFileWriting() 
        {

        }
    }
}