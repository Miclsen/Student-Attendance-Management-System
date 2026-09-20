using ScrumAttendanceSystem;

static void DeleteStudent()
{
    Console.Write("Enter Student ID to delete: ");
    if (int.TryParse(Console.ReadLine(), out int studentId))
    {
        // Remove the student from the list
        int removedCount = students.RemoveAll(s => s.Studentid == studentId);

        if (removedCount > 0)
        {
            // Clean up associated attendance records
            attendanceRecords.RemoveAll(a => a.Studentld == studentId);
            Console.WriteLine("Student successfully deleted!\n");
        }
        else
        {
            Console.WriteLine("Student ID not found.\n");
        }
    }
    else
    {
        Console.WriteLine("Invalid input! Student ID must be a number.\n");
    }
}