using System;

namespace AttendanceSystem
{
    public class AttendanceRecord
    {
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public bool Present { get; set; }
    }
}
