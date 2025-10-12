using SIMS.Interfaces;
using SIMS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.Services
{
    public class ReportModule : IReportGenerator
    {
        private static string connectionString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=SIMS_DB;Trusted_Connection=True;";


        public void GenerateReportCard(int studentId)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT s.Name, c.Name AS Course, e.Marks FROM ExamResults e JOIN Students s ON e.StudentID = s.StudentID JOIN Courses c ON e.Course = c.CourseID WHERE s.StudentID = @StudentID";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@StudentID", studentId);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            double total = 0;
            int count = 0;

            Console.WriteLine($"\nReport Card for Student ID: {studentId}");
            Console.WriteLine("------------------------------------");

            while (reader.Read())
            {
                double marks = Convert.ToDouble(reader["Marks"]);
                total += marks;
                count++;
                Console.WriteLine($"{reader["Course"]}: {marks}");
            }

            double gpa = count > 0 ? total / count : 0;
            Console.WriteLine($"GPA: {gpa:F2}");
        }
    }
}
