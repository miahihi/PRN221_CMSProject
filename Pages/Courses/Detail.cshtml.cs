using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Project.Models;

namespace Project.Pages.Courses
{
    public class DetailModel : PageModel
    {
        public User userlogin { get; set; }
        public Models.Course c { get; set; }
        //public Models.Enrollment e { get; set; }
        public List<WeekLesson> wl { get; set; }
        public void OnGet(int id)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                string loginID = HttpContext.Request.Cookies["loginId"];

                userlogin = context.Users.FirstOrDefault(x => x.UserId == int.Parse(loginID));
                c = context.Courses.FirstOrDefault(x => x.CourseId == id);
                wl = context.WeekLessons
                    .Where(x => x.CourseId == id)
                    .OrderBy(x => x.StartDate)
                    .ToList();
                //e = context.Enrollments.FirstOrDefault(x => x.CourseId == id && x.UserId == userlogin.UserId);
 
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
