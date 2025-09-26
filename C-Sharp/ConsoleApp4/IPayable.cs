using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal interface IPayable
    {
        void ProcessPayment(decimal amount);
    }
    class CreditCardPayment : IPayable
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing credit card payment of {amount:C}");
        }
    }
    public class  UPIPayment : IPayable
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing UPI payment of {amount:C}");
        }
    }
}
