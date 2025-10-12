using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Services
{
    public class AssignCourseModule
    {
        private static Dictionary<int, List<Course>> studentCourses = new Dictionary<int, List<Course>>();

        public static void AssignCourse()
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Console.Write("Enter Course Name: ");
            string courseName = Console.ReadLine();

            Console.Write("Enter Course Credits: ");
            int credits = int.Parse(Console.ReadLine());

            Course course = new Course
            {
                CourseID = courseId,
                Name = courseName,
                Credits = credits
            };

            if (!studentCourses.ContainsKey(studentId))
                studentCourses[studentId] = new List<Course>();

            studentCourses[studentId].Add(course);
            Console.WriteLine($"✅ Course '{courseName}' assigned to Student {studentId}");
        }
    }
}

