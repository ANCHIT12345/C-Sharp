using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Inventory___Billing_System__Retail_Store_.Services
{
    internal class Customer
    {
        private static string ConnectionString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Inventory_Billing;Trusted_Connection=True;";
        public static void AddCustomer()
        {
            Console.WriteLine("Enter Customer Name:");
            string customerName = Console.ReadLine();
            Console.WriteLine("Enter Customer Phone Number:");
            string customerPhone = Console.ReadLine();
            string query = "INSERT INTO Customers (Name, PhoneNo) VALUES (@CustomerName, @PhoneNumber)";
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CustomerName", customerName);
                        cmd.Parameters.AddWithValue("@Email", customerEmail);
                        cmd.Parameters.AddWithValue("@PhoneNumber", customerPhone);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Customer added successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding customer: " + ex.Message);
            }
        }
        public static void ViewCustomers()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string query = "SELECT CustomerID, Name, PhoneNo FROM Customers";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        Console.WriteLine("\n--- Customer List ---");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["CustomerID"]}. {reader["Name"]} - Phone: {reader["PhoneNo"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error viewing customers: " + ex.Message);
            }
        }
    }
}
