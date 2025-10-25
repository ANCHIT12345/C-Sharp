using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Inventory___Billing_System__Retail_Store_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== Inventory & Billing System =====");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product");
            Console.WriteLine("3. View Products");
            Console.WriteLine("4. Add Customer");
            Console.WriteLine("5. View Customers");
            Console.WriteLine("6. Create Bill");
            Console.WriteLine("7. Export Invoice to File");
            Console.WriteLine("8. View Daily Sales Report");
            Console.WriteLine();
            Console.Write("9. Exit ");
            Console.Write("Select an option (1-9): ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Services.Products.AddProduct();
                    break;
                case 2:
                    Services.Products.UpdateProduct();
                    break;
                case 3:
                    Services.Products.ViewProductCatalog();
                    break;
                case 4:
                    Services.Customer.AddCustomer();
                    break;
                case 5:
                    Services.Customer.ViewCustomers();
                    break;
                case 6:
                    Console.WriteLine("Enter Customer ID");
                    int customerId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter product name for the bill: ");
                    string productname = Console.ReadLine();
                    Console.Write("Enter quantity for the bill: ");
                    int qty = Convert.ToInt32(Console.ReadLine());
                    Services.Billing.CreateBill(qty, productname, customerId);
                    break;
                case 7:
                    Services.Billing.ExportInvoiceToFile();
                    break;
                case 8:
                    Services.Billing.ViewDailySalesReport();
                    break;
                case 9:
                    Console.WriteLine("Exiting the application. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
}