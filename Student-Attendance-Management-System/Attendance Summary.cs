static void AttendanceSummary()
{
    Console.Write("Enter Student ID: ");
    int studentId = int.Parse(Console.ReadLine());

    // Find student and filter their attendance records
    Student student = students.Find(s => s.Studentid == studentId);
    var records = attendanceRecords.FindAll(a => a.Studentld == studentId);

    // Count attendance statuses
    int present = records.Count(a => a.Status == "Present");
    int absent = records.Count(a => a.Status == "Absent");
    int late = records.Count(a => a.Status == "Late");

    // Formula: (Present / Total Records) * 100
    double percentage = ((double)present / records.Count) * 100;

    Console.WriteLine("====================================");
    Console.WriteLine("         ATTENDANCE SUMMARY         ");
    Console.WriteLine("====================================");
    Console.WriteLine("Student ID: " + student.Studentid);
    Console.WriteLine("Student Name: " + student.Name);
    Console.WriteLine("Present: " + present);
    Console.WriteLine("Absent: " + absent);
    Console.WriteLine("Late: " + late);
    Console.WriteLine("Attendance Percentage: " + percentage.ToString("F0") + "%");
    Console.WriteLine("====================================\n");
}