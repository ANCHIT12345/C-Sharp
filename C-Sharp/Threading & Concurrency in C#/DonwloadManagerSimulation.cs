using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading___Concurrency_in_C_
{
    internal class DonwloadManagerSimulation
    {
        public static void Run()
        {
            string[] files = { "File1.zip", "File2.zip", "File3.zip", "File4.zip", "File5.zip" };
            foreach (var file in files)
            {
                new Thread(() => DownloadFile(file)).Start();
            }
        }
        static void DownloadFile(string file)
        {
            Random rand = new Random();
            int total = 100;
            int done = 0;
            Console.WriteLine($"Starting download of {file}");
            while (done < total)
            {
                Thread.Sleep(rand.Next(100, 500));
                done += rand.Next(5, 20);
                if (done > total) done = total;
                Console.WriteLine($"{file}: {done}% downloaded");
            }
            Console.WriteLine($"{file} download completed!");
        }

        //public static async Task Main()
        //{
        //    string[] files = { "File1.zip", "File2.zip", "File3.zip", "File4.zip", "File5.zip" };
        //    Task[] download = new Task[files.Length];
        //    Random random = new Random();
        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        string file = files[i];
        //        download[i] = Task.Run(() => DownloadFile2(file, random));
        //    }
        //    await Task.WhenAll(download);
        //    Console.WriteLine("\nAll downlaods completed");
        //}
        //static void DownloadFile2(string file, Random rand)
        //{
        //    int total = 100;
        //    int done = 0;
        //    int speed = rand.Next(10, 25);
        //    Console.WriteLine($"Starting download of {file}");
        //    while (done < total)
        //    {
        //        Thread.Sleep(200);
        //        done += speed;
        //        if (done > total) done = total;
        //        Console.WriteLine($"{file}: {done}% downloaded");
        //    }
        //    Console.WriteLine($"{file} download completed!");
        //}
    }
}
