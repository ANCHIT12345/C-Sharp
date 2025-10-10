using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Login_Packages
{
    internal class SearchBookprogram
    {
        public void SearchBookViaSP()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Database connection successful.");

                    Console.Write("Enter the Book ID to search: ");
                    int bookId = Convert.ToInt32(Console.ReadLine());

                    using (SqlCommand command = new SqlCommand("usp_SearchBookById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookId", bookId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Console.WriteLine("\nBook found:");
                                while (reader.Read())
                                {
                                    Console.WriteLine($"ID: {reader["BookId"]}");
                                    Console.WriteLine($"Title: {reader["Title"]}");
                                    Console.WriteLine($"Author: {reader["Author"]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nNo book found with that ID.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

        }
    }
}
