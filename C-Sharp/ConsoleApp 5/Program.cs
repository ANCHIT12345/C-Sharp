using System;
using System.Data.SqlClient;
namespace dbmsApp1
{
    class Program
    {
        static void Main(String[] args)
        {
            //DisplayConnectionLibrary();
            //DisplayBooksLibrary();
            //DisplayStudent();
            //DisplayStudentCount();
            //DisplayTotalBooksCount();
            InsertNewStudent();
            Console.ReadKey();
        }
        public static void DisplayConnectionLibrary()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Database Connection Status-" + conn.State);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplayBooksLibrary()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Books;", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"BookId -{reader["BookId"]},Title -{reader["Title"]},Author -{reader["Author"]},Genre -{reader["Genre"]},Price -{reader["Price"]},StockQty -{reader["StockQty"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplayStudent()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Students;", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Student ID -{reader["StudentID"]},Student Name -{reader["StudentName"]},Age -{reader["Age"]},Course -{reader["Course"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplayStudentCount()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT Count(*) FROM Students;", conn);
                    int studentCount = (int)sqlCmd.ExecuteScalar();
                    Console.WriteLine($"Student Count : {studentCount}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplayTotalBooksCount()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT Count(*) FROM Books;", conn);
                    int BooksCount = (int)sqlCmd.ExecuteScalar();
                    Console.WriteLine($"Books Count : {BooksCount}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void InsertNewStudent()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT Count(*) FROM Books;", conn);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }

    }
}