using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class Submission
    {
        public int? UserId { get; set; }
        public int? AssignId { get; set; }
        public DateTime? UploadTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? DueDate { get; set; }
        public int Int { get; set; }

        public virtual Assignment? Assign { get; set; }
        public virtual User? User { get; set; }
    }
}
