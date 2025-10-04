using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Hospital_Packages
{
    internal class DeleteOperations
    {
        public void deletePatient()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Enter Patient ID to delete:");
                    int patientID = Convert.ToInt32(Console.ReadLine());
                    SqlCommand sqlCmd = new SqlCommand($"DELETE FROM Patients where PatientID = {patientID};", conn);
                    int rowAffected = sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Patient Deleted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public void deleteDoctor()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Enter Doctor ID to delete:");
                    int docID = Convert.ToInt32(Console.ReadLine());
                    SqlCommand sqlCmd = new SqlCommand($"DELETE FROM Doctors where DoctorID = {docID};", conn);
                    int rowAffected = sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Doctor Deleted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
    }
}
