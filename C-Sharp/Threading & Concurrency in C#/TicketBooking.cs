using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Threading___Concurrency_in_C_
{
    class TicketBooking1
    {
        private int availableTickets = 5;
        public void BookTicket(string counterName)
        {
            while (availableTickets > 0)
            {
                Console.WriteLine($"{counterName} checking available tickets: {availableTickets}");
                Thread.Sleep(100);
                availableTickets--;
                Console.WriteLine($"{counterName} booked a ticket. Tickets left: {availableTickets}");
            }
        }
    }
    class TicketBooking2 
    {
        private int availableTickets = 5;
        private static Mutex mutex = new Mutex();
        
        public void BookTicketWithMutex(string counterName)
        {
            while (true)
            {
                mutex.WaitOne();
                if (availableTickets > 0)
                {
                    Console.WriteLine($"{counterName} Booking a ticket .");
                    Thread.Sleep(200);
                    availableTickets--;
                    Console.WriteLine($"{counterName} Booked Successfully. Remaining : {availableTickets}");
                    mutex.ReleaseMutex();
                }
                else
                {
                    mutex.ReleaseMutex();
                    break;
                }
            }
        }
    }
    class TicketBooking
    {
        public static void Run()
        {
            TicketBooking1 ticketBooking1 = new TicketBooking1();
            Thread counter1 = new Thread(() => ticketBooking1.BookTicket("Counter 1"));
            Thread counter2 = new Thread(() => ticketBooking1.BookTicket("Counter 2"));
            Thread counter3 = new Thread(() => ticketBooking1.BookTicket("Counter 3"));
            counter1.Start();
            counter2.Start();
            counter3.Start();
            counter1.Join();
            counter2.Join();
            counter3.Join();
            Console.WriteLine("All tickets booked. (without mutex)");

            TicketBooking2 ticketBooking2 = new TicketBooking2();
            Thread counter4 = new Thread(() => ticketBooking2.BookTicketWithMutex("Counter 1"));
            Thread counter5 = new Thread(() => ticketBooking2.BookTicketWithMutex("Counter 2"));
            Thread counter6 = new Thread(() => ticketBooking2.BookTicketWithMutex("Counter 3"));
            counter4.Start();
            counter5.Start();
            counter6.Start();
            counter4.Join();
            counter5.Join();
            counter6.Join();
            Console.WriteLine("All tickets booked. (with mutex)");
        }
    }

}
