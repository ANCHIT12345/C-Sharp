using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoginSystem.Login_Packages
{
    internal class InsertNewUser
    {
        public void InsertNewUserInfo()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    Console.WriteLine("Database Connection Status-" + conn.State);
                    Console.WriteLine("Enter Username:");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter Password:");
                    string pass = Console.ReadLine();

                    SqlCommand sqlCmd = new SqlCommand("usp_InsertUser", conn);
                    sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("usp_InsertUser", conn);

                    sqlCmd.Parameters.AddWithValue("@username", username);
                    sqlCmd.Parameters.AddWithValue("@password", pass);

                    int rowAffected = sqlCmd.ExecuteNonQuery();

                    //Console.WriteLine("Database Connection Status-"+conn.State);
                    Console.WriteLine("Employee Inserted Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
    }
}
