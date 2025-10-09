using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Login_Packages
{
    internal class Login
    {
        public void UserLogin()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Database Connection Status - " + conn.State);

                    Console.Write("Enter Username: ");
                    string username = Console.ReadLine();

                    Console.Write("Enter Password: ");
                    string pass = Console.ReadLine();

                    using (SqlCommand sqlCmd = new SqlCommand("usp_LoginUser", conn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@username", username);
                        sqlCmd.Parameters.AddWithValue("@password", pass);

                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read(); // move to first row
                                Console.WriteLine("\nLogin Successful! Welcome, " + reader["Username"].ToString());
                            }
                            else
                            {
                                Console.WriteLine("\nLogin Failed: Invalid username or password.");
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
