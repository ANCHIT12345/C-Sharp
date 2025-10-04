using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Hospital_Packages
{
    internal class DisplayAppointments
    {
        public static void DisplayAppointmentsByPatientID()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection Successful");
                    Console.WriteLine("Enter Patient ID:");
                    int patientID = Convert.ToInt32(Console.ReadLine());
                    SqlCommand sqlCmd = new SqlCommand($"SELECT * FROM Appointments WHERE PatientID={patientID}", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Console.WriteLine("AppointmentID\tPatientID\tDoctorID\tAppointmentDate");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["AppointmentID"]}\t{reader["PatientID"]}\t{reader["DoctorID"]}\t{reader["AppointmentDate"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No appointments found for the given Patient ID.");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public static void DisplayAppointmentsByDoctorID()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection Successful");
                    Console.WriteLine("Enter Doctor ID:");
                    int doctorID = Convert.ToInt32(Console.ReadLine());
                    SqlCommand sqlCmd = new SqlCommand($"SELECT * FROM Appointments WHERE DoctorID={doctorID}", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Console.WriteLine("AppointmentID\tPatientID\tDoctorID\tAppointmentDate");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["AppointmentID"]}\t{reader["PatientID"]}\t{reader["DoctorID"]}\t{reader["AppointmentDate"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No appointments found for the given Doctor ID.");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
    }
}
