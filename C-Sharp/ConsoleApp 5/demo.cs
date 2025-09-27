//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DatabaseProject
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            //DisplayEmployee();
//            //InsertEmployee();

//            GetMaxEmployeeSalary();

//            Console.Read();

//        }
//        public static void DisplayEmployee()
//        {
//            string connString = "Data Source=ISSLT1220;Initial Catalog=Demo;User id=sa;Password=inube@123";

//            using (SqlConnection conn = new SqlConnection(connString))
//            {
//                try
//                {
//                    conn.Open();


//                    SqlCommand sqlCmd = new SqlCommand("select EmployeeName,Design,salary from Employee", conn);
//                    SqlDataReader reader = sqlCmd.ExecuteReader();

//                    while (reader.Read())
//                    {
//                        Console.WriteLine($"Name -{reader["EmployeeName"]},Designation -{reader["Design"]}");
//                    }
//                    //Console.WriteLine("Database Connection Status-"+conn.State);
//                    //Console.WriteLine("Database Connection Successful!");
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Connection Failed: " + ex.Message);
//                }
//            }


//        }
//        public static void GetMaxEmployeeSalary()
//        {
//            string connString = "Data Source=ISSLT1220;Initial Catalog=Demo;User id=sa;Password=inube@123";

//            using (SqlConnection conn = new SqlConnection(connString))
//            {
//                try
//                {
//                    conn.Open();


//                    SqlCommand sqlCmd = new SqlCommand("select Max(salary) from [dbo].[Employee]", conn);
//                    //float MaxSalary = (float)sqlCmd.ExecuteScalar();

//                    decimal maxValue = (decimal)sqlCmd.ExecuteScalar();



//                    Console.WriteLine($"Employee Count -{maxValue}");
//                    //Console.WriteLine("Database Connection Status-"+conn.State);
//                    //Console.WriteLine("Database Connection Successful!");
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Connection Failed: " + ex.Message);
//                }
//            }


//        }
//        public static void InsertEmployee()
//        {
//            string connString = "Data Source=ISSLT1220;Initial Catalog=Demo;User id=sa;Password=inube@123";

//            using (SqlConnection conn = new SqlConnection(connString))
//            {
//                try
//                {
//                    conn.Open();


//                    SqlCommand sqlCmd = new SqlCommand("INSERT INTO Employee(EmployeeName,Design,salary,ManagerId,DepartmentId) " +
//                        "values('Amarpreet', 'Web Develoer', 110000.00, 4, 1)", conn);

//                    int rowAffected = sqlCmd.ExecuteNonQuery();

//                    //Console.WriteLine("Database Connection Status-"+conn.State);
//                    Console.WriteLine("Employee Inserted Successfully!");
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Connection Failed: " + ex.Message);
//                }
//            }


//        }
//    }
//}
