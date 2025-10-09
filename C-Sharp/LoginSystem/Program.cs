using System;
using System.Data.SqlClient;
using LoginSystem.Login_Packages;
namespace dbmsApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }
        public void Run()
        {
            Console.WriteLine("Enter the Operation to be performed");
            Console.WriteLine("1. Insert New User");
            Console.WriteLine("2. User Login");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Insert insert = new Insert();
                    insert.InsertNewUserInfo();
                    break;
                case 2:
                    Login login = new Login();
                    login.UserLogin();
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }
    }
    class Insert
    {
        public void InsertNewUserInfo()
        {
            LoginSystem.Login_Packages.InsertNewUser insertNewUser = new LoginSystem.Login_Packages.InsertNewUser();
            insertNewUser.InsertNewUserInfo();
            Program program = new Program();
            program.Run();
        }
    }
    class Login
    {
        public void UserLogin()
        {
            LoginSystem.Login_Packages.Login login = new LoginSystem.Login_Packages.Login();
            login.UserLogin();
            Program program = new Program();
            program.Run();
        }
    }
}