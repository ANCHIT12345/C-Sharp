using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Hospital_Packages
{
    internal class InsertOperations
    {
        public void insertPatient()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection Successful");
                    Console.WriteLine("Enter Patient Name:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter Patient Age:");
                    int age = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Patient Disease:");
                    string disease = Console.ReadLine();
                    SqlCommand sqlCmd = new SqlCommand($"INSERT INTO Patients(Name, Age, Disease) VALUES('{name}',{age},'{disease}')", conn);
                    int rowAffected = sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Patient inserted successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        public void insertDoctor()
        {
            string connString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Stored_Procedure_Assignment;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection Successful");
                    Console.WriteLine("Enter Doctor Name:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter Doctor Specialization:");
                    string specialization = Console.ReadLine();
                    SqlCommand sqlCmd = new SqlCommand($"INSERT INTO Doctors(Name, Specialization) VALUES('{name}','{specialization}')", conn);
                    int rowAffected = sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Doctor inserted successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
    }
}
