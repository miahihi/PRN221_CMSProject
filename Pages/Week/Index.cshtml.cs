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
            //id: weekid
            if (id != 0)
            {
                using (prn231_finalprojectContext context = new prn231_finalprojectContext())
                {
                    string loginID = HttpContext.Request.Cookies["loginId"];

                    userlogin = context.Users.FirstOrDefault(x => x.UserId == int.Parse(loginID));
                    wl = context.WeekLessons.FirstOrDefault(x => x.Id == id);
                    c = context.Courses.FirstOrDefault(x => x.CourseId == wl.CourseId);
                    ass = context.Assignments
                        .Where(x => x.Wlid == id).ToList();

                }
            }
        }
        public IActionResult OnPostUnenroll(int courseId)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                string loginID = HttpContext.Request.Cookies["loginId"];
                Enrollment data = context.Enrollments.FirstOrDefault(c => c.CourseId == courseId && c.UserId == int.Parse(loginID));
                context.Enrollments.Remove(data);
                context.SaveChanges();
                return RedirectToPage("/Courses/Index");
            }
        }
    }
}
