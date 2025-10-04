using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Hospital_Packages
{
    internal class UpdateOperations
    {
        public void updatePatient()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Enter Patient ID to update disease:");
                    int patientID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter new disease:");
                    string disease = Console.ReadLine();
                    SqlCommand sqlCmd = new SqlCommand($"UPDATE Patients set Disease = '{disease}' where PatientID = {patientID};", conn);
                    int rowAffected = sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Patients Disease Updated");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public void updateDoctor()
        {
            {
                string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        Console.WriteLine("Enter Doctor ID to update specialization:");
                        int docID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter new specialization:");
                        string specialization = Console.ReadLine();
                        SqlCommand sqlCmd = new SqlCommand($"UPDATE Doctors set Specialization = '{specialization}' where DoctorID = {docID};", conn);
                        int rowAffected = sqlCmd.ExecuteNonQuery();
                        Console.WriteLine("Patients Disease Updated");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Connection Failed: " + ex.Message);
                    }
                }
            }
        }
    }
}
