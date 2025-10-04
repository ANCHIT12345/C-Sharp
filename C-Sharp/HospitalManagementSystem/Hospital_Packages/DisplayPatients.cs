using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Hospital_Packages
{
    internal class DisplayPatients
    {
        public static void DisplayPatientsInfo()
        {
            Console.WriteLine("Displaying Patients Information...");
            // Code to display patients information goes here
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Patients;", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("Patient ID: " + reader["PatientID"] + ", Name: " + reader["Name"] + ", Age: " + reader["Age"] + ", Disease : " + reader["Disease"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplaTotalPatientsCount() 
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT Count(*) FROM Patients;", conn);
                    int patientcount = (int)sqlCmd.ExecuteScalar();
                    Console.WriteLine("Total Patients Count: " + patientcount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplayPatientByID()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Enter Patient ID to display:");
                    int patientID = Convert.ToInt32(Console.ReadLine());
                    SqlCommand sqlCmd = new SqlCommand($"SELECT * FROM Patients where PatientID = {patientID};", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("Patient ID: " + reader["PatientID"] + ", Name: " + reader["Name"] + ", Age: " + reader["Age"] + ", Disease : " + reader["Disease"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplayPatientsByDisease()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Enter Disease to display all patients:");
                    string disease = Console.ReadLine();
                    SqlCommand sqlCmd = new SqlCommand($"SELECT * FROM Patients where Disease = '{disease}';", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("Patient ID: " + reader["PatientID"] + ", Name: " + reader["Name"] + ", Age: " + reader["Age"] + ", Disease : " + reader["Disease"]);
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
