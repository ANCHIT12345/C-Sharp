using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading___Concurrency_in_C_
{
    internal class TPL
    {
        public static void Run()
        {
            Console.WriteLine("Starting image processing..\n");
            string[] images = { "Image1.jpg", "Image2.jpg", "Image3.jpg", "Image4.jpg", "Image5.jpg" };
            Task[] tasks = new Task[images.Length];
            for (int i = 0; i < images.Length; i++)
            {
                string img = images[i];
                tasks[i] = Task.Run(() => ProcessImage(img));
            }
            Task.WaitAll(tasks);
            Console.WriteLine("\nAll images processed.");
        }
        static void ProcessImage(string imageName)
        {
            Console.WriteLine($"[{imageName}] - Processing started at {DateTime.Now:HH:mm:ss.fff}");
            Task.Delay(2000).Wait();
            Console.WriteLine($"[{imageName}] - Processing finished at {DateTime.Now:HH:mm:ss.fff}");
        }
        public static void Run2()
        {
            string[] images = { "Image1.jpg", "Image2.jpg", "Image3.jpg", "Image4.jpg", "Image5.jpg" };
            Console.WriteLine("Starting image processing in parallel..\n");
            Parallel.ForEach(images, image =>
            {
                Console.WriteLine($"[{image}] started on Thread {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(2000);
                Console.WriteLine($"[{image}] finished on Thread {Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine("\nAll images processed in parallel.");
        }
    }
}
