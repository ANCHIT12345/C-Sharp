using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;

namespace Inventory___Billing_System__Retail_Store_.Services
{
    // 1. Interface for billing operations
    interface IBillable
    {
        void CreateBill(int qty, int customerID);
    }

    // 2. Abstract product class
    abstract class ProductBase
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }

        public abstract decimal GetDiscount(int qty);

        public override string ToString()
        {
            return $"ID: {ProductID}, Name: {Name}, Price: {Price:C}, Stock: {StockQty}";
        }
    }

    // 3. Derived product classes with custom discounts
    class Grocery : ProductBase
    {
        public override decimal GetDiscount(int qty)
        {
            // Example: 5% discount for 10 or more items
            if (qty >= 10) return 0.05m;
            return 0m;
        }
    }

    class Electronics : ProductBase
    {
        public override decimal GetDiscount(int qty)
        {
            // Example: 10% discount for 2 or more items
            if (qty >= 2) return 0.10m;
            return 0m;
        }
    }

    // 4. Billing service
    internal class Billing : IBillable
    {
        private static string ConnectionString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Inventory_Billing;Trusted_Connection=True;";
        private ProductBase Product;
        private int Quantity;

        public Billing(ProductBase product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public void CreateBill(int qty, int customerID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string updateQuery = "UPDATE Products SET StockQty = StockQty - @Quantity WHERE Name = @ProductName AND StockQty >= @Quantity";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", Product.Name);
                        cmd.Parameters.AddWithValue("@Quantity", qty);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Stock updated successfully.");
                            CreateInvoiceFile(qty, customerID);
                            LogDailySale(qty, customerID);
                        }
                        else
                        {
                            Console.WriteLine("Insufficient stock or product does not exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating bill: " + ex.Message);
            }
        }

        private void CreateInvoiceFile(int qty, int customerID)
        {
            decimal discountRate = Product.GetDiscount(qty);
            decimal totalAmount = Product.Price * qty * (1 - discountRate);

            var invoice = new
            {
                CustomerID = customerID,
                ProductID = Product.ProductID,
                ProductName = Product.Name,
                UnitPrice = Product.Price,
                Quantity = qty,
                Discount = discountRate,
                TotalAmount = totalAmount,
                Date = DateTime.Now
            };

            // Save JSON file
            string jsonFileName = $"Invoice_Customer_{customerID}_{DateTime.Now:yyyyMMddHHmmss}.json";
            string jsonContent = JsonSerializer.Serialize(invoice, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFileName, jsonContent);
            Console.WriteLine($"Invoice saved as JSON: {jsonFileName}");

            // Optional: Save as text file
            string textFileName = $"Invoice_Customer_{customerID}_{DateTime.Now:yyyyMMddHHmmss}.txt";
            string textContent = $"Invoice for Customer ID: {customerID}\n" +
                                 $"Product: {Product.Name}\n" +
                                 $"Quantity: {qty}\n" +
                                 $"Unit Price: {Product.Price:C}\n" +
                                 $"Discount: {discountRate:P}\n" +
                                 $"Total Amount: {totalAmount:C}\n" +
                                 $"Date: {DateTime.Now}\n";
            File.WriteAllText(textFileName, textContent);
        }

        private void LogDailySale(int qty, int customerID)
        {
            string logFile = $"DailySales_{DateTime.Now:yyyyMMdd}.json";

            List<object> sales = new List<object>();
            if (File.Exists(logFile))
            {
                string existingData = File.ReadAllText(logFile);
                sales = JsonSerializer.Deserialize<List<object>>(existingData) ?? new List<object>();
            }

            var sale = new
            {
                CustomerID = customerID,
                ProductID = Product.ProductID,
                ProductName = Product.Name,
                Quantity = qty,
                TotalAmount = Product.Price * qty * (1 - Product.GetDiscount(qty)),
                Date = DateTime.Now
            };

            sales.Add(sale);
            string updatedData = JsonSerializer.Serialize(sales, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(logFile, updatedData);
        }
    }
}
