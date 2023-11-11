using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class WeekLesson
    {
        public WeekLesson()
        {
            Assignments = new HashSet<Assignment>();
        }

        public int Id { get; set; }
        public int? CourseId { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }

        public virtual Course? Course { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }

        public string WeekTime(string time)
        {
            string formattedEndDate;
            if (EndDate != null)
            {
                formattedEndDate = EndDate.Value.ToString("dd/MMMM");
            }
            else
            {
                formattedEndDate = "";
            }

            string formattedStartDate;
            if (StartDate != null)
            {
                formattedStartDate = StartDate.Value.ToString("dd/MMMM");
            }
            else
            {
                formattedStartDate = "";
            }

            return formattedEndDate + " - " + formattedStartDate;
        }
        public string WeekTime()
        {
            return WeekTime("Now");
        }
        
    }
}
