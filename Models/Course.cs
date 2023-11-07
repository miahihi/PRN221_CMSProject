using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
            WeekLessons = new HashSet<WeekLesson>();
        }

        public int CourseId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? CategoryId { get; set; }

        public virtual CourseCategory? Category { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<WeekLesson> WeekLessons { get; set; }
    }
}
