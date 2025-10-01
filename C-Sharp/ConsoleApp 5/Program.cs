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
            //InsertNewStudent();
            //InsertNewBook();
            //UpdateAge();
            //UpdateBookQty();
            //DeleteStudent();
            //DeleteBook();
            //DisplayStudentDetails();
            //DisplayBookDetails();
            DisplayStudentDetailsDept();
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
                    SqlCommand sqlCmd = new SqlCommand("INSERT INTO Students(StudentID, StudentName, Age, Course) VALUES(4,'Vikas',22,'MCA')", conn);
                    int rowAffected = sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Student inserted successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }

        }
        public static void InsertNewBook()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("INSERT INTO Books( Title, Author, Genre, Price, StockQty,DiscountApplied) VALUES('test','test','test',1000,10,0)", conn);
                    int rowAffected = sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Book inserted successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }

        }
        public static void UpdateAge()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("UPDATE Students set age = 50 where Studentname = 'vikas';", conn);
                    int rowAffected = sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Student Age Updated");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }

        }
        public static void UpdateBookQty()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("UPDATE Books set StockQty = 20 where Title='test';", conn);
                    int rowAffected = sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Book Qty Updated");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }

        }
        public static void DeleteStudent()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("DELETE FROM Students WHERE StudentID = 4", conn);
                    int rowAffected = sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Student DELETED");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }

        }
        public static void DeleteBook()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("DELETE FROM Books WHERE BookID = 7", conn);
                    int rowAffected = sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Book DELETED");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }

        }
        public static void DisplayStudentDetails()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.Write("Enter The student ID to display students details: ");
                    int studID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    SqlCommand sqlCmd = new SqlCommand($"SELECT * FROM Students WHERE StudentID = {studID};", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Read();
                    Console.WriteLine($"Student ID -{reader["StudentID"]},Student Name -{reader["StudentName"]},Age -{reader["Age"]},Course -{reader["Course"]}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplayBookDetails()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.Write("Enter The Book Name to display Book details: ");
                    string bookName = Console.ReadLine();
                    Console.WriteLine();
                    SqlCommand sqlCmd = new SqlCommand($"SELECT * FROM Books WHERE Title = '{bookName}';", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Read();
                    Console.WriteLine($"BookId -{reader["BookId"]},Title -{reader["Title"]},Author -{reader["Author"]},Genre -{reader["Genre"]},Price -{reader["Price"]},StockQty -{reader["StockQty"]}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplayStudentDetailsDept()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.Write("Enter The Dept Name to display all students enrolled details: ");
                    string Course = Console.ReadLine();
                    Console.WriteLine();
                    SqlCommand sqlCmd = new SqlCommand($"SELECT * FROM Students WHERE Course = '{Course}';", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while(reader.Read())
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
    }
}