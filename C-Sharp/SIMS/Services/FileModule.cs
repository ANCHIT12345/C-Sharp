using System;
using System.Data.SqlClient;
using System.IO;

namespace SIMS.Modules
{
    public class FileModule
    {
        private static string connectionString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=SIMS_DB;Trusted_Connection=True;";


        public static void ExportStudentsToFile()
        {
            string path = "StudentsList.csv";
            using StreamWriter writer = new StreamWriter(path);
            using SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT StudentID, Name, Email FROM Students";
            using SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            writer.WriteLine("StudentID,Name,Email");
            while (reader.Read())
            {
                writer.WriteLine($"{reader["StudentID"]},{reader["Name"]},{reader["Email"]}");
            }

            Console.WriteLine($"✅ Student list exported to {path}");
        }
    }
}
