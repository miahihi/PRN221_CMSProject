using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class Assignment
    {
        public Assignment()
        {
            Submissions = new HashSet<Submission>();
        }

        public int Id { get; set; }
        public int? Wlid { get; set; }
        public string? AssignmentName { get; set; }
        public int? AssignmentFilesize { get; set; }
        public byte[]? Attachment { get; set; }

        public virtual WeekLesson? Wl { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
