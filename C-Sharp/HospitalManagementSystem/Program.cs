using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem;
namespace dbmsApp1
{
    class HospitalMain
    {
        static void Main(String[] args)
        { 
            HospitalManagementSystem.Hospital_Packages.DbConnection.DisplayConnectionHospitalDB();
            HospitalManagementSystem.Hospital_Packages.DisplayPatients.DisplayPatientsInfo();
            HospitalManagementSystem.Hospital_Packages.DisplayDoctors.DisplayDoctorsInfo();
        }
    }
}