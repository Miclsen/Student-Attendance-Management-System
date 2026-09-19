using System;

namespace AttendanceSystem
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;

        public Student() { }

        public Student(int id, string name, string course)
        {
            StudentId = id;
            Name = name;
            Course = course;
        }
    }
}
