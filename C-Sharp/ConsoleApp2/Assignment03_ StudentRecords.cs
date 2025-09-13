using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp3
{
    internal class Assignment03__StudentRecords
    {
        public static void Run()
        {
            Dictionary<int,string> StudentRecord = new Dictionary<int,string>();
            StudentRecord.Add(1, "Alice");
            StudentRecord.Add(2, "Bob");
            StudentRecord.Add(3, "Charlie");
            StudentRecord.Add(4, "Diana");
            StudentRecord.Add(5, "Ethan");
            Console.WriteLine("Enter the roll_no:");
            int roll_no = Convert.ToInt32(Console.ReadLine());
            if (StudentRecord.ContainsKey(roll_no))
            {
                Console.WriteLine("Student Name: " + StudentRecord[roll_no]);
            }
            else
            {
                Console.WriteLine("No record found for the given roll number.");
            }
        }
    }
}
