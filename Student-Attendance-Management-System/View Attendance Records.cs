static void ViewAttendance()
{
    Console.WriteLine("====================================");
    Console.WriteLine("         ATTENDANCE RECORDS         "); [cite: 1]
    Console.WriteLine("------------------------------------"); [cite: 1]
    Console.WriteLine("{0,-12} {1,-20} {2,-12} {3,-10}", "Student ID", "Name", "Date", "Status"); [cite: 1]

    foreach (var record in attendanceRecords)
    {
        Student student = students.Find(s => s.Studentid == record.Studentld);

        Console.WriteLine("{0,-12} {1,-20} {2,-12} {3,-10}",
            record.Studentld,
            student.Name,
            record.Date.ToString("MM/dd/yyyy"),
            record.Status); [cite: 1]
    }

    Console.WriteLine("====================================\n");
}