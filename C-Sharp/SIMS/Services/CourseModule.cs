using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS.Models;

namespace SIMS.Services
{
    public class CourseModule
    {
        public static List<Course> Courses = new List<Course>();

        public static void AddCourse()
        {
            Console.Write("Enter Course Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Credits: ");
            int credits = int.Parse(Console.ReadLine());

            int courseId = Courses.Count + 1;

            Courses.Add(new Course
            {
                CourseID = courseId,
                Name = name,
                Credits = credits
            });

            Console.WriteLine($"✅ Course '{name}' added successfully!");
        }

        public static void ViewCourses()
        {
            Console.WriteLine("\n--- Available Courses ---");
            foreach (var course in Courses)
            {
                Console.WriteLine($"{course.CourseID}. {course.Name} ({course.Credits} Credits)");
            }
        }
    }
}
