using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Hospital_Packages
{
    internal class DisplayDoctors
    {
        public static void DisplayDoctorsInfo()
        {
            Console.WriteLine("Displaying Doctors Information...");
            // Code to display doctors information goes here
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Doctors;", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("Doctor ID: " + reader["DoctorID"] + ", Name: " + reader["Name"] + ", Specialty: " + reader["Specialization "]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplaTotalDoctorCount()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT Count(*) FROM Doctors;", conn);
                    int doctorcount = (int)sqlCmd.ExecuteScalar();
                    Console.WriteLine("Total Doctor Count: " + doctorcount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplayDoctorByID()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Enter Doctor ID to search:");
                    int docID = Convert.ToInt32(Console.ReadLine());
                    SqlCommand sqlCmd = new SqlCommand($"SELECT * FROM Doctors where DoctorID = {docID};", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine("Doctor ID: " + reader["DoctorID"] + ", Name: " + reader["Name"] + ", Specialty: " + reader["Specialization "]);
                    }
                    else
                    {
                        Console.WriteLine("Doctor not found with ID: " + docID);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplayDoctorsBySpecialization()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Enter Specialization to search:");
                    string specialization = Console.ReadLine();
                    SqlCommand sqlCmd = new SqlCommand($"SELECT * FROM Doctors where Specialization = '{specialization}';", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    bool found = false;
                    while (reader.Read())
                    {
                        found = true;
                        Console.WriteLine("Doctor ID: " + reader["DoctorID"] + ", Name: " + reader["Name"] + ", Specialty: " + reader["Specialization "]);
                    }
                    if (!found)
                    {
                        Console.WriteLine("No doctors found with specialization: " + specialization);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplayMostConsultedDoctor()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT TOP 1 DoctorID, COUNT(*) AS ConsultationCount FROM Appointments GROUP BY DoctorID ORDER BY ConsultationCount DESC;", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine("Most Consulted Doctor ID: " + reader["DoctorID"] + ", Consultation Count: " + reader["ConsultationCount"]);
                    }
                    else
                    {
                        Console.WriteLine("No consultation records found.");
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
