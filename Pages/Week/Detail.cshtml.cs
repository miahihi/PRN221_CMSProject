using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages.Week
{
    public class DetailModel : PageModel
    {
        public User userlogin { get; set; }
        public Models.Course c { get; set; }
        //public Models.Enrollment e { get; set; }
        public Assignment ass { get; set; }
        public WeekLesson wl { get; set; }
        public List<Submission> smList { get; set; }
        public Submission sm { get; set; }
        public void OnGet(int id)
        {
            //assignId
            if (id != 0)
            {
                using (prn231_finalprojectContext context = new prn231_finalprojectContext())
                {
                    string loginID = HttpContext.Request.Cookies["loginId"];
                    ass = context.Assignments.FirstOrDefault(x => x.Id == id);
                    wl = context.WeekLessons.FirstOrDefault(x => x.Id == ass.Wlid);
                    c = context.Courses.FirstOrDefault(x => x.CourseId == wl.CourseId);
                    sm = context.Submissions.FirstOrDefault(x=>x.AssignId==id && x.UserId==int.Parse(loginID));
                }
            }
        }
      
    }
}
