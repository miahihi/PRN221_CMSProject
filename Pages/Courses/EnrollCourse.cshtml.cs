using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages.Courses
{
    public class EnrollCourseModel : PageModel
    {
        [BindProperty]
        public User userlogin { get; set; }
        public Models.Course c { get; set; }
        public void OnGet(int id = 0)
        {
            if (id != 0)
            {
                using (prn231_finalprojectContext context = new prn231_finalprojectContext())
                {
                    string loginID = HttpContext.Request.Cookies["loginId"];

                    userlogin = context.Users.FirstOrDefault(x => x.UserId == int.Parse(loginID));
                    c = context.Courses.FirstOrDefault(x => x.CourseId == id);
                }
            }
        }
        public IActionResult OnPostSubmitEnroll(int courseId)
        {
            try
            {
                using (prn231_finalprojectContext context = new prn231_finalprojectContext())
                {
                    string loginID = HttpContext.Request.Cookies["loginId"];
                    userlogin = context.Users.FirstOrDefault(x => x.UserId == int.Parse(loginID));
                    context.Enrollments.Add(new Enrollment
                    {
                        CourseId = courseId,
                        UserId = userlogin.UserId,
                        EnrollTime = DateTime.Now
                    }) ;
                    context.SaveChanges();
                    return RedirectToPage("/Courses/Detail", new { id = courseId });
                }
            }
            catch
            {
                ViewData["mess"] = "Enroll Error!!!";
                return Page();
            }
        }
    }
}
