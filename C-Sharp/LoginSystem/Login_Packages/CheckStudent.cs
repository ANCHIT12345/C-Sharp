using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Login_Packages
{
    internal class CheckStudent
    {
        public void CheckStudentInfo()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Database Connection Status - " + conn.State);

                    Console.Write("Enter Student ID: ");
                    int studentId = Convert.ToInt32(Console.ReadLine());

                    using (SqlCommand sqlCmd = new SqlCommand("usp_CheckStudent", conn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@studentId", studentId);

                        // Add a parameter to capture the return value
                        SqlParameter returnValue = new SqlParameter();
                        returnValue.Direction = System.Data.ParameterDirection.ReturnValue;
                        sqlCmd.Parameters.Add(returnValue);

                        sqlCmd.ExecuteNonQuery();

                        int result = (int)returnValue.Value;

                        if (result == 1)
                        {
                            Console.WriteLine("\nStudent exists in the database.");
                        }
                        else
                        {
                            Console.WriteLine("\nNo student found with the provided ID.");
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
