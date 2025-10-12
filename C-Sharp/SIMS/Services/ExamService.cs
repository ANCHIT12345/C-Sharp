using System;
using System.Data.SqlClient;

namespace SIMS.Services
{
    public class ExamModule
    {
        private static string connectionString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=SIMS_DB;Trusted_Connection=True;";


        public static void RecordExamMarks()
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Console.Write("Enter Marks: ");
            double marks = double.Parse(Console.ReadLine());

            string query = "INSERT INTO ExamResults (StudentID, Course, Marks) VALUES (@StudentID, @Course, @Marks)";
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@StudentID", studentId);
            cmd.Parameters.AddWithValue("@Course", courseId);
            cmd.Parameters.AddWithValue("@Marks", marks);
            con.Open();
            cmd.ExecuteNonQuery();

            Console.WriteLine("✅ Exam marks recorded successfully!");
        }
    }
}
