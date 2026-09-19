using System;
using System.Collections.Generic;

namespace AttendanceSystem
{
    class Program
    {
        static void Main()
        {
            var students = new List<Student>();
            var attendance = new List<AttendanceRecord>();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("-- STUDENT ATTENDANCE MANAGEMENT --");
                Console.WriteLine("1. Add student");
                Console.WriteLine("2. Update student");
                Console.WriteLine("3. Search student");
                Console.WriteLine("4. Record attendance");
                Console.WriteLine("5. View attendance");
                Console.WriteLine("0. Exit");
                Console.Write("Select option: ");
                string? opt = Console.ReadLine();

                switch ((opt ?? string.Empty).Trim())
                {
                    case "1":
                        AddStudent.Add(students);
                        break;
                    case "2":
                        UpdateStudent.Update(students);
                        break;
                    case "3":
                        SearchStudent.Search(students);
                        break;
                    case "4":
                        RecordAttendance.Record(students, attendance);
                        break;
                    case "5":
                        RecordAttendance.View(students, attendance);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Unknown option.");
                        break;
                }
            }
        }
    }
}
