using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS.Models;

namespace SIMS.Services
{
    public class StudentModule
    {
        private static string connectionString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=SIMS_DB;Trusted_Connection=True;";

        public static void AddStudent()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            string query = "INSERT INTO Students(Name, Email, Course) VALUES (@Name, @Email, @Course)";
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Course", courseId);
            con.Open();
            cmd.ExecuteNonQuery();

            Console.WriteLine("Student added successfully!");
        }

        public static void ViewStudents()
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT s.StudentID, s.Name, s.Email, c.Name AS CourseName FROM Students s JOIN Courses c ON s.Course = c.CourseID";
            using SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- Student List ---");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["StudentID"]}. {reader["Name"]} ({reader["Email"]}) - Course: {reader["CourseName"]}");
            }
        }
    }
}
