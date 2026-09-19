using System;
using System.Collections.Generic;

namespace AttendanceSystem
{
    public static class UpdateStudent
    {
        public static void Update(List<Student> students)
        {
            Console.WriteLine();
            Console.WriteLine("--- UPDATE STUDENT ---");
            Console.Write("Enter student ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var student = students.Find(s => s.StudentId == id);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.WriteLine($"Current name: {student.Name}");
            Console.Write("Enter new name (leave blank to keep): ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine($"Current course: {student.Course}");
            Console.Write("Enter new course (leave blank to keep): ");
            string course = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(name)) student.Name = name.Trim();
            if (!string.IsNullOrWhiteSpace(course)) student.Course = course.Trim();

            Console.WriteLine("Student information updated.");
        }
    }
}
