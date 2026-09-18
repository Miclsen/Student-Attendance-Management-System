using System;
using System.Collections.Generic;

namespace AttendanceSystem
{
    class Program
    {
        // Stores every student added to the system.
        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
           
        }

        // US02 - Displays all students in the list.
        static void ViewStudents()
        {
            Console.WriteLine();
            Console.WriteLine("--- LIST OF STUDENTS ---");

            // Check if the list is empty.
            if (students.Count == 0)
            {
                Console.WriteLine("No students have been added yet.");
                return;
            }

            // Print a simple table of all students.
            Console.WriteLine("ID".PadRight(8) + "Name".PadRight(25) + "Course");
            Console.WriteLine("----------------------------------------");

            foreach (Student student in students)
            {
                Console.WriteLine(student.StudentId.ToString().PadRight(8)
                    + student.Name.PadRight(25)
                    + student.Course);
            }

            Console.WriteLine();
            Console.WriteLine("Total students: " + students.Count);
        }
    }
}
