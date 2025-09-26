using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal interface IDatabase
    {
        void Connect();
        void Disconnect();
    }
    class SqlDatabase : IDatabase
    {
        public void Connect()
        {
            Console.WriteLine("Connected to SQL Database.");
        }
        public void Disconnect()
        {
            Console.WriteLine("Disconnected from SQL Database.");
        }
    }
    class OracleDatabase : IDatabase
    {
        public void Connect()
        {
            Console.WriteLine("Connected to Oracle Database.");
        }
        public void Disconnect()
        {
            Console.WriteLine("Disconnected from Oracle Database.");
        }
    }
}
