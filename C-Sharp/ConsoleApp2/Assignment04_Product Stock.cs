using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp4
{
    internal class Assignment04_Product_Stock
    {
        public static void Run()
        {
            Dictionary<string, int> productStock = new Dictionary<string, int>();
            productStock["Laptop"] = 50;
            productStock["Smartphone"] = 200;
            productStock["Tablet"] = 150;
            productStock["Headphones"] = 300;
            productStock["Smartwatch"] = 100;
            Console.WriteLine("Product Stock Levels:");
            foreach (var item in productStock)
            {
                Console.WriteLine($"{item.Key}: {item.Value} units");
            }
            string response;
            do
            {
                Console.WriteLine("Enter the product you want to order:");
                string procutName = Console.ReadLine();
                Console.WriteLine("Enter the quantity");
                int quantity = Convert.ToInt32(Console.ReadLine());
                if (productStock.ContainsKey(procutName))
                {
                    if (productStock[procutName] >= quantity)
                    {
                        productStock[procutName] -= quantity;
                        Console.WriteLine($"Order placed for {quantity} units of {procutName}. Remaining stock: {productStock[procutName]} units.");
                    }
                    else
                    {
                        Console.WriteLine($"Insufficient stock for {procutName}. Available stock: {productStock[procutName]} units.");
                    }
                }
                else
                {
                    Console.WriteLine($"Product {procutName} not found in stock.");
                }
                Console.WriteLine("Do you want to place another order? (yes/no)");
                response = Console.ReadLine().ToLower();
            } while (response != "no");
        }
    }
}
