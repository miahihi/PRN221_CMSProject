using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages.Week
{
    public class IndexModel : PageModel
    {
        public User userlogin { get; set; }
        public Models.Course c { get; set; }
        //public Models.Enrollment e { get; set; }
        public List<Assignment> ass { get; set; }
        public WeekLesson wl { get; set; }
        public void OnGet(int id)
        {
            if (id != 0)
            {
                using (prn231_finalprojectContext context = new prn231_finalprojectContext())
                {
                    string loginID = HttpContext.Request.Cookies["loginId"];

                    userlogin = context.Users.FirstOrDefault(x => x.UserId == int.Parse(loginID));
                    c = context.Courses.FirstOrDefault(x => x.CourseId == id);
                    wl = context.WeekLessons.FirstOrDefault(x => x.Id == id);
                    ass = context.Assignments
                        .Where(x => x.Wlid == id).ToList();

                }
            }
        }
    }
}
