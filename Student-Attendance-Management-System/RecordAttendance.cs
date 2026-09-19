using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceSystem
{
    public static class RecordAttendance
    {
        public static void Record(List<Student> students, List<AttendanceRecord> records)
        {
            Console.WriteLine();
            Console.WriteLine("--- RECORD ATTENDANCE ---");
            if (students.Count == 0)
            {
                Console.WriteLine("No students available. Add students first.");
                return;
            }

            Console.Write("Enter date (yyyy-MM-dd) or leave blank for today: ");
            string? input = Console.ReadLine();
            DateTime date = DateTime.Today;
            if (!string.IsNullOrWhiteSpace(input) && !DateTime.TryParse(input, out date))
            {
                Console.WriteLine("Invalid date format. Using today.");
                date = DateTime.Today;
            }

            Console.WriteLine($"Recording attendance for {date:yyyy-MM-dd}");

            foreach (var s in students)
            {
                Console.Write($"{s.StudentId}\t{s.Name}\tPresent? (y/n): ");
                string? resp = Console.ReadLine();
                bool present = (resp ?? string.Empty).Trim().ToLowerInvariant() == "y";

                // Remove any existing record for that student/date
                records.RemoveAll(r => r.StudentId == s.StudentId && r.Date.Date == date.Date);

                records.Add(new AttendanceRecord
                {
                    StudentId = s.StudentId,
                    Date = date.Date,
                    Present = present
                });
            }

            Console.WriteLine("Attendance recorded.");
        }

        public static void View(List<Student> students, List<AttendanceRecord> records)
        {
            Console.WriteLine();
            Console.WriteLine("--- VIEW ATTENDANCE ---");
            Console.Write("Search by student ID or date (id/date): ");
            string option = (Console.ReadLine() ?? string.Empty).Trim().ToLowerInvariant();

            if (option == "id")
            {
                Console.Write("Enter student ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID.");
                    return;
                }

                var student = students.FirstOrDefault(s => s.StudentId == id);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }

                var entries = records.Where(r => r.StudentId == id).OrderBy(r => r.Date).ToList();
                if (!entries.Any())
                {
                    Console.WriteLine("No attendance records for this student.");
                    return;
                }

                Console.WriteLine("Date\t\tPresent");
                foreach (var e in entries)
                {
                    Console.WriteLine($"{e.Date:yyyy-MM-dd}\t{(e.Present ? "Yes" : "No")}");
                }
            }
            else
            {
                Console.Write("Enter date (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    Console.WriteLine("Invalid date.");
                    return;
                }

                var entries = records.Where(r => r.Date.Date == date.Date).ToList();
                if (!entries.Any())
                {
                    Console.WriteLine("No attendance records for that date.");
                    return;
                }

                Console.WriteLine("ID\tName\tPresent");
                foreach (var e in entries)
                {
                    var student = students.FirstOrDefault(s => s.StudentId == e.StudentId);
                    string name = student?.Name ?? "(unknown)";
                    Console.WriteLine($"{e.StudentId}\t{name}\t{(e.Present ? "Yes" : "No")}");
                }
            }
        }
    }
}
