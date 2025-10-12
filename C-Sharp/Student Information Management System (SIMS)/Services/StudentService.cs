using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_Management_System__SIMS_.Services
{
    public class StudentService
    {
        private string connectionString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=SIMS_DB;Trusted_Connection=True;";
        public void AddStudent()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
        }
    }
}
