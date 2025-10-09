using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //DisplayEmployee();
            //InsertEmployee();
            //InsertEmployeeWithParameters();
            //GetEmployeeCountWithOutputType();
            //GetMaxEmployeeSalary();
            GetEmployeeCountWithOutputType();
            //GetEmployeeCountByOutputType();
            //InsertEmployeeWithTransaction();

            Console.Read();

        }
        public static void DisplayEmployee()
        {
            string connString = "Data Source=ISSLT1220;Initial Catalog=Demo;User id=sa;Password=inube@123";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();


                    SqlCommand sqlCmd = new SqlCommand("select EmployeeName,Design,salary from Employee", conn);
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"Name -{reader["EmployeeName"]},Designation -{reader["Design"]}");
                    }
                    //Console.WriteLine("Database Connection Status-"+conn.State);
                    //Console.WriteLine("Database Connection Successful!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }


        }
        public static void GetMaxEmployeeSalary()
        {
            string connString = "Data Source=ISSLT1220;Initial Catalog=Demo;User id=sa;Password=inube@123";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();


                    SqlCommand sqlCmd = new SqlCommand("select Max(salary) from [dbo].[Employee]", conn);
                    //float MaxSalary = (float)sqlCmd.ExecuteScalar();

                    decimal maxValue = (decimal)sqlCmd.ExecuteScalar();



                    Console.WriteLine($"Employee Count -{maxValue}");
                    //Console.WriteLine("Database Connection Status-"+conn.State);
                    //Console.WriteLine("Database Connection Successful!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }


        }
        public static void InsertEmployee()
        {
            string connString = "Data Source=ISSLT1220;Initial Catalog=Demo;User id=sa;Password=inube@123";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();


                    SqlCommand sqlCmd = new SqlCommand("INSERT INTO Employee(EmployeeName,Design,salary,ManagerId,DepartmentId) " +
                        "values('Amarpreet', 'Web Develoer', 110000.00, 4, 1)", conn);

                    int rowAffected = sqlCmd.ExecuteNonQuery();

                    //Console.WriteLine("Database Connection Status-"+conn.State);
                    Console.WriteLine("Employee Inserted Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }


        }

        public static void InsertEmployeeWithParameters()
        {
            string connString = "Data Source=ISSLT1220;Initial Catalog=Demo;User id=sa;Password=inube@123";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();


                    SqlCommand sqlCmd = new SqlCommand("INSERT INTO Employee(EmployeeName,Design,salary,ManagerId,DepartmentId) " +
                        "values(@name, @design, @salary, @manager_id, @department_id)", conn);

                    sqlCmd.Parameters.AddWithValue("@name", "Lokesh");
                    sqlCmd.Parameters.AddWithValue("@design", "Web Develoer");
                    sqlCmd.Parameters.AddWithValue("@salary", 112000.00);
                    sqlCmd.Parameters.AddWithValue("@manager_id", 4);
                    sqlCmd.Parameters.AddWithValue("@department_id", 1);

                    int rowAffected = sqlCmd.ExecuteNonQuery();

                    //Console.WriteLine("Database Connection Status-"+conn.State);
                    Console.WriteLine("Employee Inserted Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }


        }
        public static void InsertEmployeeByStoredProcedure()
        {
            string connString = "Data Source=ISSLT1220;Initial Catalog=Demo;User id=sa;Password=inube@123";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();


                    SqlCommand sqlCmd = new SqlCommand("usp_InsertEmployee", conn);

                    sqlCmd.Parameters.AddWithValue("@name", "Lokesh");
                    sqlCmd.Parameters.AddWithValue("@design", "Web Develoer");
                    sqlCmd.Parameters.AddWithValue("@salary", 112000.00);
                    sqlCmd.Parameters.AddWithValue("@manager_id", 4);
                    sqlCmd.Parameters.AddWithValue("@department_id", 1);

                    int rowAffected = sqlCmd.ExecuteNonQuery();

                    //Console.WriteLine("Database Connection Status-"+conn.State);
                    Console.WriteLine("Employee Inserted Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }
        }
        //Output parameters example
        /*declare @empCount int
        exec usp_GetEmployeeCount @empCount output
        print @empCount*/
        public static void GetEmployeeCountWithOutputType()
        {
            string connString = "Data Source=ISSLT1220;Initial Catalog=Demo;User id=sa;Password=inube@123";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();


                    //SqlTransaction tran = conn.BeginTransaction();

                    SqlCommand sqlCmd = new SqlCommand("usp_GetEmployeeCount", conn);
                    sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;


                    //sqlCmd.Parameters.AddWithValue("@empCount",SQLDb)

                    // sqlCmd.Parameters.AddWithValue("@employee_count", System.Data.SqlDbType.Int).Direction == System.Data.ParameterDirection.Output;

                    //sqlCmd.Parameters.Add("@employee_count", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.InputOutput;

                    //SqlParameter parm2 = new SqlParameter("@employee_count", System.Data.SqlDbType.Int);
                    //sqlCmd.Parameters.Add(parm2);

                    // Add output parameter
                    SqlParameter outputParam = new SqlParameter("@employee_count", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    sqlCmd.Parameters.Add(outputParam);

                    int rowAffected = sqlCmd.ExecuteNonQuery();

                    int totalCount = (int)sqlCmd.Parameters["@employee_count"].Value;

                    //Console.WriteLine("Database Connection Status-"+conn.State);
                    Console.WriteLine("Employee Inserted Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex.Message);
                }
            }


        }

        public static void GetEmployeeCountByOutputType()
        {
            string connectionString = "Data Source=ISSLT1220;Initial Catalog=Demo;User id=sa;Password=inube@123";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetEmployeeCount", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add output parameter
                    SqlParameter outputParam = new SqlParameter("@employee_count", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    conn.Open();
                    cmd.ExecuteNonQuery(); // execute stored proc

                    int employeeCount = (int)cmd.Parameters["@employee_count"].Value;

                    Console.WriteLine("Total Employee Count: " + employeeCount);
                }
            }
        }


        public static void InsertEmployeeWithTransaction()
        {
            string connString = "Data Source=ISSLT1220;Initial Catalog=Demo;User id=sa;Password=inube@123";

            using (SqlConnection conn = new SqlConnection(connString))
            {

                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    SqlCommand sqlCmd = new SqlCommand("USG_CreateEmployee", conn, tran);
                    sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCmd.Parameters.Add("@EmpName", "Pramjeet");
                    sqlCmd.Parameters.Add("@Design", "Sr. Software Engineer");
                    sqlCmd.Parameters.Add("@Salary", 112000.00);
                    sqlCmd.Parameters.Add("@ManagerId", 4);
                    sqlCmd.Parameters.Add("@DepartmentId", 1);
                    sqlCmd.Parameters.Add("@JoiningDate", DateTime.Now);

                    int rowAffected = sqlCmd.ExecuteNonQuery();

                    SqlCommand sqlCmdUpdt = new SqlCommand("usp_UpdateEmployee", conn, tran);
                    sqlCmdUpdt.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCmdUpdt.Parameters.AddWithValue("@manager_id", 3);
                    sqlCmdUpdt.Parameters.AddWithValue("@employee_id", 3006);

                    int rowUpdated = sqlCmdUpdt.ExecuteNonQuery();

                    tran.Commit();

                    Console.WriteLine("Employee Inserted/Updated Successfully!");
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    Console.WriteLine("Transaction Failed: " + ex.Message);
                }
            }
        }


    }
}
