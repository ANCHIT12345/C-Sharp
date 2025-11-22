using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading___Concurrency_in_C_
{
    internal class Producer_Consumer_Problem
    {
        private readonly Queue<int> _buffer = new Queue<int>();
        private readonly int capacity = 5;
        private readonly object lockobj = new object();

        public void Produce()
        {
            int item = 0;
            while (true)
            {
                lock (lockobj)
                {
                    while (_buffer.Count >= capacity)
                    {
                        Monitor.Wait(lockobj);
                    }
                    _buffer.Enqueue(item);
                    Console.WriteLine($"Produced: {item}");
                    item++;
                    Monitor.PulseAll(lockobj);
                }
                Thread.Sleep(500);
            }
        }
        public void Consume()
        {
            while (true)
            {
                int item;
                lock (lockobj)
                {
                    while (_buffer.Count == 0)
                    {
                        Monitor.Wait(lockobj);
                    }
                    item = _buffer.Dequeue();
                    Console.WriteLine($"Consumed: {item}");
                    Monitor.PulseAll(lockobj);
                }
                Thread.Sleep(1000);
            }
        }
    }
    class ProducerConsumerDemo
    {
        public static void Run()
        {
            Producer_Consumer_Problem pc = new Producer_Consumer_Problem();
            Thread producerThread = new Thread(pc.Produce);
            Thread producerThread2 = new Thread(pc.Produce);
            Thread consumerThread = new Thread(pc.Consume);
            Thread consumerThread2 = new Thread(pc.Consume);
            producerThread.Start();
            producerThread2.Start();
            consumerThread.Start();
            consumerThread2.Start();
            Console.WriteLine("Program ended (Producer-Consumer)");
        }
    }
}
