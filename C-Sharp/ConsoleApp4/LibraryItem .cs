using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    abstract class LibraryItem
    {
        public string Title { get; set; }
        public LibraryItem(string title)
        {
            Title = title;
        }
        public abstract void DisplayInfo();
    }
    class Book : LibraryItem
    {
        public string Author { get; set; }
        public Book(string title, string author) : base(title)
        {
            Author = author;
        }
        public override void DisplayInfo()
        {
            Console.WriteLine($"Book: {Title}, Author: {Author}");
        }
    }
    class Magazine : LibraryItem
    {
        public int IssueNumber { get; set; }
        public Magazine(string title, int issueNumber) : base(title)
        {
            IssueNumber = issueNumber;
        }
        public override void DisplayInfo()
        {
            Console.WriteLine($"Magazine: {Title}, Issue Number: {IssueNumber}");
        }
    }
}
