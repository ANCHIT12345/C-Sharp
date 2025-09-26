using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    interface Iprintable
    {
        void Print(string document);
    }
    interface IScannable
    {
        void Scan(string document);
    }
    interface ICopiable
    {
        void Copy(string document);
    }
    class MultifunctionPrinter : Iprintable, IScannable, ICopiable
    {
        public void Print(string document)
        {
            Console.WriteLine($"Printing document: {document}");
        }
        public void Scan(string document)
        {
            Console.WriteLine($"Scanning document: {document}");
        }
        public void Copy(string document)
        {
            Console.WriteLine($"Copying document: {document}");
        }
    }
}
