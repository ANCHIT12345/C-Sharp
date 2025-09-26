using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal interface IReservable
    {
        void Reserve();
    }
    class Book2 : IReservable
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Book2(string title, string author)
        {
            Title = title;
            Author = author;
        }
        public void Reserve()
        {
            Console.WriteLine($"Book '{Title}' by {Author} has been reserved.");
        }
    }
    class DVD : IReservable
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public DVD(string title, string director)
        {
            Title = title;
            Director = director;
        }
        public void Reserve()
        {
            Console.WriteLine($"DVD '{Title}' directed by {Director} has been reserved.");
        }
    }
}
