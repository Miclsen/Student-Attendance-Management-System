using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrumAttendanceSystem
{
    public static class Validation
    {
        public static int ReadPositiveInt(string prompt)
        {
            int number;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out number) && number > 0)
                {
                    return number;
                }
                Console.WriteLine("Error: Please enter a valid positive number.");
            }
        }
        public static int ReadUniqueStudentId(List<Student> students)
        {
            while (true)
            {
                int id = ReadPositiveInt("Enter Student ID: ");
                if (students.Any(s => s.StudentId == id))
                {
                    Console.WriteLine("Error: Student ID already exists. Try a different ID.");
                }
                else
                {
                    return id;
                }
            }
        }
        public static Student ReadExistingStudent(List<Student> students)
        {
            int id = ReadPositiveInt("Enter Student ID: ");
            Student student = students.Find(s => s.StudentId == id);

            if (student == null)
            {
                Console.WriteLine("Error: Student ID not found in the system.");
            }
            return student;
        }
        public static string ReadNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }
                Console.WriteLine("Error: Field cannot be left empty. Please try again.");
            }
        }
        public static DateTime ReadValidDate()
        {
            Console.Write("Enter Date (MM/dd/yyyy) or press Enter for Today: ");
            string dateInput = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(dateInput))
            {
                return DateTime.Now;
            }

            if (DateTime.TryParse(dateInput, out DateTime date))
            {
                return date;
            }

            Console.WriteLine("Invalid date format. Defaulting to today's date.");
            return DateTime.Now;
        }
        public static string ReadAttendanceStatus()
        {
            while (true)
            {
                Console.WriteLine("1. Present\n2. Absent\n3. Late");
                Console.Write("Enter attendance status choice: ");
                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1": return "Present";
                    case "2": return "Absent";
                    case "3": return "Late";
                    default:
                        Console.WriteLine("Error: Invalid choice. Select 1, 2, or 3.\n");
                        break;
                }
            }
        }
    }
}