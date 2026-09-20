using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceSystem
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;

        public Student()
        {
        }

        public Student(int studentId, string name, string course)
        {
            StudentId = studentId;
            Name = name;
            Course = course;
        }
    }

    public class AttendanceRecord
    {
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = "Present";

        public bool Present
        {
            get => string.Equals(Status, "Present", StringComparison.OrdinalIgnoreCase);
            set => Status = value ? "Present" : "Absent";
        }
    }

    public static class Validation
    {
        public static int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                {
                    return value;
                }

                Console.WriteLine("Invalid input. Please enter a positive number.");
            }
        }

        public static int ReadUniqueStudentId(List<Student> students)
        {
            while (true)
            {
                int id = ReadPositiveInt("Enter Student ID: ");
                if (students.Any(s => s.StudentId == id))
                {
                    Console.WriteLine("Student ID already exists. Please choose another one.");
                    continue;
                }

                return id;
            }
        }

        public static Student? ReadExistingStudent(List<Student> students)
        {
            int id = ReadPositiveInt("Enter Student ID: ");
            return students.FirstOrDefault(s => s.StudentId == id);
        }

        public static string ReadNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? value = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value.Trim();
                }

                Console.WriteLine("This field cannot be empty.");
            }
        }

        public static DateTime ReadValidDate()
        {
            Console.Write("Enter date (MM/dd/yyyy) or press Enter for today: ");
            string? dateInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(dateInput))
            {
                return DateTime.Today;
            }

            if (DateTime.TryParse(dateInput, out DateTime date))
            {
                return date;
            }

            Console.WriteLine("Invalid date format. Using today's date instead.");
            return DateTime.Today;
        }

        public static string ReadAttendanceStatus()
        {
            while (true)
            {
                Console.WriteLine("1. Present");
                Console.WriteLine("2. Absent");
                Console.WriteLine("3. Late");
                Console.Write("Select attendance status: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        return "Present";
                    case "2":
                        return "Absent";
                    case "3":
                        return "Late";
                    default:
                        Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                        break;
                }
            }
        }
    }

    internal static class Program
    {
        private static readonly List<Student> students = new();
        private static readonly List<AttendanceRecord> attendanceRecords = new();

        private static void Main()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== STUDENT ATTENDANCE MANAGEMENT SYSTEM ===");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Search Student");
                Console.WriteLine("6. Record Attendance");
                Console.WriteLine("7. View Attendance");
                Console.WriteLine("8. Attendance Summary");
                Console.WriteLine("9. Exit");
                Console.Write("Enter choice: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ViewStudents();
                        break;
                    case "3":
                        UpdateStudent();
                        break;
                    case "4":
                        DeleteStudent();
                        break;
                    case "5":
                        SearchStudent();
                        break;
                    case "6":
                        RecordAttendance();
                        break;
                    case "7":
                        ViewAttendance();
                        break;
                    case "8":
                        GenerateAttendanceSummary();
                        break;
                    case "9":
                        Console.WriteLine("Exiting application...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose a valid menu number.");
                        break;
                }
            }
        }

        private static void AddStudent()
        {
            Console.WriteLine();
            Console.WriteLine("--- ADD NEW STUDENT ---");

            int id = Validation.ReadUniqueStudentId(students);
            string name = Validation.ReadNonEmptyString("Enter student name: ");
            string course = Validation.ReadNonEmptyString("Enter course: ");

            students.Add(new Student(id, name, course));
            Console.WriteLine($"Student added successfully with ID {id}.");
        }

        private static void ViewStudents()
        {
            Console.WriteLine();
            Console.WriteLine("--- VIEW ALL STUDENTS ---");

            if (students.Count == 0)
            {
                Console.WriteLine("No registered students yet.");
                return;
            }

            Console.WriteLine("{0,-10} {1,-25} {2,-15}", "ID", "Name", "Course");
            Console.WriteLine(new string('-', 50));

            foreach (Student student in students)
            {
                Console.WriteLine("{0,-10} {1,-25} {2,-15}", student.StudentId, student.Name, student.Course);
            }

            Console.WriteLine($"Total Students: {students.Count}");
        }

        private static void UpdateStudent()
        {
            Console.WriteLine();
            Console.WriteLine("--- UPDATE STUDENT ---");

            Student? student = Validation.ReadExistingStudent(students);
            if (student == null)
            {
                Console.WriteLine("Student ID not found.");
                return;
            }

            Console.WriteLine($"Current name: {student.Name}");
            string newName = Validation.ReadNonEmptyString("Enter new name (leave blank to keep): ");
            Console.WriteLine($"Current course: {student.Course}");
            string newCourse = Validation.ReadNonEmptyString("Enter new course (leave blank to keep): ");

            if (!string.IsNullOrWhiteSpace(newName))
            {
                student.Name = newName;
            }

            if (!string.IsNullOrWhiteSpace(newCourse))
            {
                student.Course = newCourse;
            }

            Console.WriteLine("Student information updated successfully.");
        }

        private static void DeleteStudent()
        {
            Console.WriteLine();
            Console.WriteLine("--- DELETE STUDENT ---");

            Student? student = Validation.ReadExistingStudent(students);
            if (student == null)
            {
                Console.WriteLine("Student ID not found.");
                return;
            }

            students.Remove(student);
            attendanceRecords.RemoveAll(record => record.StudentId == student.StudentId);
            Console.WriteLine($"Student {student.Name} (ID: {student.StudentId}) deleted successfully.");
        }

        private static void SearchStudent()
        {
            Console.WriteLine();
            Console.WriteLine("--- SEARCH STUDENT ---");
            Console.Write("Search by ID or Name? (id/name): ");
            string option = Console.ReadLine() ?? string.Empty;

            if (option.Equals("id", StringComparison.OrdinalIgnoreCase))
            {
                int id = Validation.ReadPositiveInt("Enter student ID: ");
                Student? student = students.FirstOrDefault(s => s.StudentId == id);

                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }

                Console.WriteLine("ID\tName\tCourse");
                Console.WriteLine($"{student.StudentId}\t{student.Name}\t{student.Course}");
                return;
            }

            string name = Validation.ReadNonEmptyString("Enter name to search: ");
            List<Student> matches = students
                .Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matches.Count == 0)
            {
                Console.WriteLine("No students found matching that name.");
                return;
            }

            Console.WriteLine("ID\tName\tCourse");
            foreach (Student student in matches)
            {
                Console.WriteLine($"{student.StudentId}\t{student.Name}\t{student.Course}");
            }
        }

        private static void RecordAttendance()
        {
            Console.WriteLine();
            Console.WriteLine("--- RECORD ATTENDANCE ---");

            if (students.Count == 0)
            {
                Console.WriteLine("No students available. Add students first.");
                return;
            }

            DateTime date = Validation.ReadValidDate();

            foreach (Student student in students)
            {
                Console.Write($"{student.StudentId}\t{student.Name}\tAttendance status for {date:yyyy-MM-dd}: ");
                string status = Validation.ReadAttendanceStatus();

                attendanceRecords.RemoveAll(record => record.StudentId == student.StudentId && record.Date.Date == date.Date);
                attendanceRecords.Add(new AttendanceRecord
                {
                    StudentId = student.StudentId,
                    Date = date,
                    Status = status
                });

                Console.WriteLine($"Attendance recorded for {student.Name}: {status}");
            }
        }

        private static void ViewAttendance()
        {
            Console.WriteLine();
            Console.WriteLine("--- VIEW ATTENDANCE ---");

            if (attendanceRecords.Count == 0)
            {
                Console.WriteLine("No attendance records found.");
                return;
            }

            Console.WriteLine("Student ID\tName\t\tDate\t\tStatus");
            Console.WriteLine(new string('-', 70));

            foreach (AttendanceRecord record in attendanceRecords.OrderBy(r => r.Date).ThenBy(r => r.StudentId))
            {
                Student? student = students.FirstOrDefault(s => s.StudentId == record.StudentId);
                string studentName = student?.Name ?? "Unknown";
                Console.WriteLine($"{record.StudentId}\t\t{studentName}\t\t{record.Date:yyyy-MM-dd}\t{record.Status}");
            }
        }

        private static void GenerateAttendanceSummary()
        {
            Console.WriteLine();
            Console.WriteLine("--- ATTENDANCE SUMMARY ---");

            int studentId = Validation.ReadPositiveInt("Enter Student ID: ");
            Student? student = students.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            List<AttendanceRecord> records = attendanceRecords
                .Where(record => record.StudentId == studentId)
                .ToList();

            if (records.Count == 0)
            {
                Console.WriteLine("No attendance records found for this student.");
                return;
            }

            int present = records.Count(r => string.Equals(r.Status, "Present", StringComparison.OrdinalIgnoreCase));
            int absent = records.Count(r => string.Equals(r.Status, "Absent", StringComparison.OrdinalIgnoreCase));
            int late = records.Count(r => string.Equals(r.Status, "Late", StringComparison.OrdinalIgnoreCase));
            double percentage = ((double)present / records.Count) * 100;

            Console.WriteLine($"Student ID: {student.StudentId}");
            Console.WriteLine($"Student Name: {student.Name}");
            Console.WriteLine($"Present: {present}");
            Console.WriteLine($"Absent: {absent}");
            Console.WriteLine($"Late: {late}");
            Console.WriteLine($"Attendance Percentage: {percentage:F0}%");
        }
    }
}