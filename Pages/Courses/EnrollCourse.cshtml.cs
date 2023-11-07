using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages.Courses
{
    public class EnrollCourseModel : PageModel
    {
        public User userlogin { get; set; }
        public Course c { get; set; }
        public void OnGet(int? courseId)
        {
            if (courseId!=0)
            {
                using (prn231_finalprojectContext context = new prn231_finalprojectContext())
                {
                    string loginID = HttpContext.Request.Cookies["loginId"];

                    userlogin = context.Users.FirstOrDefault(x => x.UserId == int.Parse(loginID));
                    c = context.Courses.FirstOrDefault(x => x.CourseId == courseId);
                }
            }
        }
        public IActionResult OnPostSubmitEnroll(string courseId)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                string loginID = HttpContext.Request.Cookies["loginId"];
                Enrollment data = context.Enrollments.FirstOrDefault(c => c.CourseId == int.Parse(courseId) && c.UserId == int.Parse(loginID));
                context.Enrollments.Add(data);
                context.SaveChanges();
                return Page();
            }
        }
    }
}
