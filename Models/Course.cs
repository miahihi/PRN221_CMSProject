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

        public bool isAlreadyEnroll(int UserID)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                Enrollment? enrollment = context.Enrollments.FirstOrDefault(p => p.UserId == UserID && p.CourseId == CourseId);
                if (enrollment == null)
                {
                    //not enroll
                    return true;
                }
                return false;
            }
        }
    }
}
