using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading___Concurrency_in_C_
{
    class Hotel 
    {
        public string Name { get; }
        public Hotel(string name) => Name = name;
    }
    internal class HotelRoomBooking_DeadLock
    {
        static object hotel1 = new object();
        static object hotel2 = new object();
        public static void Run()
        {
            Thread t1 = new Thread(BookHotelAFirst);
            Thread t2 = new Thread(BookHotelBFirst);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.WriteLine("Program ended (DeadLock)");
        }
        static void BookHotelAFirst()
        {
            lock (hotel1)
            {
                Console.WriteLine("Locked Hotel A, trying to lock Hotel B...");
                Thread.Sleep(1000);
                Console.WriteLine("Trying to lock Hotel B...");
                lock (hotel2)
                {
                    Console.WriteLine("Booked both Hotel A and Hotel B");
                }
            }
        }
        static void BookHotelBFirst()
        {
            lock (hotel2)
            {
                Console.WriteLine("Locked Hotel B, trying to lock Hotel A...");
                Thread.Sleep(1000);
                Console.WriteLine("Trying to lock Hotel A...");
                lock (hotel1)
                {
                    Console.WriteLine("Booked both Hotel B and Hotel A");
                }
            }
        }
    }
    internal class HotelRoomBooking_Solution
    {
        static object hotel1 = new object();
        static object hotel2 = new object();
        public static void Run()
        {
            Thread t1 = new Thread(BookHotelsInOrder);
            Thread t2 = new Thread(BookHotelsInOrder);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.WriteLine("Program ended (No DeadLock)");
        }
        static void BookHotelsInOrder()
        {
            lock (hotel1)
            {
                Console.WriteLine("Locked Hotel A, trying to lock Hotel B...");
                Thread.Sleep(1000);
                Console.WriteLine("Trying to lock Hotel B...");
                lock (hotel2)
                {
                    Console.WriteLine("Booked both Hotel A and Hotel B");
                }
            }
        }
    }
}
