using System;
using System.Collections.Generic;

namespace AttendanceSystem
{
    public static class AddStudent
    {
        private static int nextId = 1;

        public static void Add(List<Student> students)
        {
            Console.WriteLine();
            Console.WriteLine("--- ADD NEW STUDENT ---");
            Console.Write("Enter student name: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter course: ");
            string course = Console.ReadLine() ?? string.Empty;

            var student = new Student(nextId++, name.Trim(), course.Trim());
            students.Add(student);

            Console.WriteLine($"Student added with ID {student.StudentId}.");
        }
    }
}
