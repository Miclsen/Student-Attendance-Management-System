using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceSystem
{
    public static class SearchStudent
    {
        public static void Search(List<Student> students)
        {
            Console.WriteLine();
            Console.WriteLine("--- SEARCH STUDENT ---");
            Console.Write("Search by ID or Name? (id/name): ");
            string option = (Console.ReadLine() ?? string.Empty).Trim().ToLowerInvariant();

            if (option == "id")
            {
                Console.Write("Enter student ID: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var found = students.FirstOrDefault(s => s.StudentId == id);
                    if (found != null)
                    {
                        Console.WriteLine("ID\tName\tCourse");
                        Console.WriteLine($"{found.StudentId}\t{found.Name}\t{found.Course}");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID.");
                }
            }
            else
            {
                Console.Write("Enter name to search: ");
                string name = (Console.ReadLine() ?? string.Empty).Trim();
                var matches = students.Where(s => s.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                if (matches.Count == 0)
                {
                    Console.WriteLine("No students found matching that name.");
                }
                else
                {
                    Console.WriteLine("ID\tName\tCourse");
                    foreach (var s in matches)
                    {
                        Console.WriteLine($"{s.StudentId}\t{s.Name}\t{s.Course}");
                    }
                }
            }
        }
    }
}
