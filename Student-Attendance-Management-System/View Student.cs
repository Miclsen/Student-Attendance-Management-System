using ScrumAttendanceSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Attendance_Management_System
{
    internal class View_Student
    {
    }
}
namespace AttendanceSystem
{
    public static class ViewStudent
    {
        public static void View(List<Student> students)
        {
            Console.WriteLine();
            Console.WriteLine("--- VIEW ALL STUDENTS ---");

            if (students == null || students.Count == 0)
            {
                Console.WriteLine("No student records found.");
            }
            else
            {
                Console.WriteLine("{0,-10} {1,-25} {2,-15}", "ID", "Name", "Course");
                Console.WriteLine(new string('-', 50));

                foreach (var student in students)
                {
                    Console.WriteLine("{0,-10} {1,-25} {2,-15}", student.StudentId, student.Name, student.Course);
                }

                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"Total Students: {students.Count}");
            }
        }
    }
}