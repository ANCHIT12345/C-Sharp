using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory___Billing_System__Retail_Store_.Services
{
    internal class Products
    {
        private static string ConnectionString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Inventory_Billing;Trusted_Connection=True;";
        public static void AddProduct() 
        {
            Console.WriteLine("Enter Product Name:");
            string productName = Console.ReadLine();
            Console.WriteLine("Enter Product Price:");
            decimal productPrice = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter Product Stock Quantity:");
            int productStock = Convert.ToInt32(Console.ReadLine());
            string query = "INSERT INTO products (Name, Price, StockQty) VALUES (@ProductName, @Price, @StockQuantity)";
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@Price", productPrice);
                        cmd.Parameters.AddWithValue("@StockQuantity", productStock);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Product added successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding product: " + ex.Message);
            }
        }
        public static void UpdateProduct()
        {
            Console.WriteLine("Enter Product ID to Update:");
            int productId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter New Product Name:");
            string productName = Console.ReadLine();
            Console.WriteLine("Enter New Product Price:");
            decimal productPrice = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter New Product Stock Quantity:");
            int productStock = Convert.ToInt32(Console.ReadLine());
            string query = "UPDATE products SET Name = @ProductName, Price = @Price, StockQty = @StockQuantity WHERE ProductID = @ProductID";
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@Price", productPrice);
                        cmd.Parameters.AddWithValue("@StockQuantity", productStock);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Product updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Product not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating product: " + ex.Message);
            }
        }
        public static void ViewProductCatalog()
        {
            string query = "SELECT ProductID, Name, Price, StockQty FROM products";
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        Console.WriteLine("\n--- Product Catalog ---");
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["ProductID"]}, Name: {reader["Name"]}, Price: {reader["Price"]}, Stock Quantity: {reader["StockQty"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error viewing product catalog: " + ex.Message);
            }
        }
    }
}
