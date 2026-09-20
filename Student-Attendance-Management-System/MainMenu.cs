using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Attendance_Management_System
{
    internal class MainMenu
    {
    }
}
namespace ScrumAttendanceSystem
{
    class Student
    {
        public int Studentid { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
    }

    class Attendance
    {
        public int Studentld { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }

    class Program
    {
        static List<Student> students = new List<Student>();
        static List<Attendance> attendanceRecords = new List<Attendance>();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n=== STUDENT ATTENDANCE MANAGEMENT SYSTEM ===");
                Console.WriteLine("1. Add Student\n2. View Students\n3. Search Student");
                Console.WriteLine("4. Record Attendance\n5. View Attendance\n6. Attendance Summary\n7. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": ViewStudents(); break;
                    case "3": SearchStudent(); break;
                    case "4": RecordAttendance(); break;
                    case "5": ViewAttendance(); break;
                    case "6": GenerateAttendanceSummary(); break;
                    case "7": Console.WriteLine("Exiting application..."); return;
                    default: Console.WriteLine("Invalid choice!"); break;
                }
            }
        }

        static void AddStudent()
        {
            Console.Write("Enter Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Invalid ID!");
                return;
            }

            foreach (var s in students)
            {
                if (s.Studentid == id) { Console.WriteLine("ID already exists!"); return; }
            }

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Course: ");
            string course = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(course))
            {
                Console.WriteLine("Input fields cannot be blank!");
                return;
            }

            students.Add(new Student { Studentid = id, Name = name, Course = course });
            Console.WriteLine("Student added successfully!");
        }

        static void ViewStudents()
        {
            if (students.Count == 0) { Console.WriteLine("No registered students."); return; }
            Console.WriteLine("\nID\tName\t\tCourse");
            Console.WriteLine("-----------------------------------");
            foreach (var s in students)
                Console.WriteLine($"{s.Studentid}\t{s.Name}\t\t{s.Course}");
        }

        static void SearchStudent()
        {
            Console.Write("Enter Student ID or Name: ");
            string query = Console.ReadLine();
            bool found = false;

            foreach (var s in students)
            {
                if (s.Studentid.ToString() == query || s.Name.Equals(query, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Found -> ID: {s.Studentid} | Name: {s.Name} | Course: {s.Course}");
                    found = true;
                }
            }
            if (!found) Console.WriteLine("Student not found.");
        }

        static void RecordAttendance()
        {
            Console.Write("Enter Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID!"); return; }

            Student student = null;
            foreach (var s in students)
            {
                if (s.Studentid == id) { student = s; break; }
            }

            if (student == null) { Console.WriteLine("Student ID not found!"); return; }

            Console.WriteLine("Select Status: 1. Present | 2. Absent | 3. Late");
            Console.Write("Choice: ");
            string stInput = Console.ReadLine();

            string status = stInput == "1" ? "Present" : stInput == "2" ? "Absent" : stInput == "3" ? "Late" : null;
            if (status == null) { Console.WriteLine("Invalid status selection!"); return; }

            attendanceRecords.Add(new Attendance { Studentld = id, Date = DateTime.Now, Status = status });
            Console.WriteLine($"Attendance recorded as '{status}' for {student.Name}!");
        }

        static void ViewAttendance()
        {
            if (attendanceRecords.Count == 0) { Console.WriteLine("No attendance records."); return; }
            Console.WriteLine("\nStudent ID\tDate\t\tStatus");
            Console.WriteLine("------------------------------------------");
            foreach (var a in attendanceRecords)
                Console.WriteLine($"{a.Studentld}\t\t{a.Date:MM/dd/yyyy}\t{a.Status}");
        }

        static void GenerateAttendanceSummary()
        {
            Console.Write("Enter Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID!"); return; }

            Student student = null;
            foreach (var s in students)
            {
                if (s.Studentid == id) { student = s; break; }
            }

            if (student == null) { Console.WriteLine("Student not found!"); return; }

            int total = 0, present = 0, absent = 0, late = 0;
            foreach (var a in attendanceRecords)
            {
                if (a.Studentld == id)
                {
                    total++;
                    if (a.Status == "Present") present++;
                    else if (a.Status == "Absent") absent++;
                    else if (a.Status == "Late") late++;
                }
            }

            if (total == 0) { Console.WriteLine("No attendance data recorded for this student."); return; }

            double percentage = ((double)present / total) * 100;

            Console.WriteLine($"\n--- SUMMARY FOR {student.Name} (ID: {student.Studentid}) ---");
            Console.WriteLine($"Present: {present} | Absent: {absent} | Late: {late}");
            Console.WriteLine($"Attendance Percentage: {percentage:F0}%");
        }
    }
}