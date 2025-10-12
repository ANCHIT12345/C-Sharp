using SIMS.Modules;
using SIMS.Services;
using System;
using System.IO;

namespace SIMS
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n===== Student Information System =====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Students");
                Console.WriteLine("3. Assign Course");
                Console.WriteLine("4. Record Exam Marks");
                Console.WriteLine("5. Generate Report Card");
                Console.WriteLine("6. Export Student List to File");
                Console.WriteLine("7. Exit");

                Console.Write("Choose an option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1: StudentModule.AddStudent(); break;
                    case 2: StudentModule.ViewStudents(); break;
                    case 3: AssignCourseModule.AssignCourse(); break;
                    case 4: ExamModule.RecordExamMarks(); break;
                    case 5:
                        Console.Write("Enter Student ID: ");
                        int id = int.Parse(Console.ReadLine());
                        new ReportModule().GenerateReportCard(id);
                        break;
                    case 6: FileModule.ExportStudentsToFile(); break;
                    case 7: return;
                    default: Console.WriteLine("Invalid option!"); break;
                }
            }
        }
    }
}
