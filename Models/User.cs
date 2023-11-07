using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class User
    {
        public User()
        {
            Enrollments = new HashSet<Enrollment>();
            Submissions = new HashSet<Submission>();
        }

        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? Type { get; set; }
        public string? Fullname { get; set; }
        public string? Mssv { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
