using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Pages.Enroll
{
    public class IndexModel : PageModel
    {
        private readonly prn231_finalprojectContext context;
        public User userlogin { get; set; }
        public IndexModel()
        {
            context = new prn231_finalprojectContext();
        }
        public List<CourseCategory> courseCategories { get; set; }
        public List<Enrollment> enrollments { get; set; }
        public List<Course> results { get; set; }
        [BindProperty]
        public string searchValue { get; set; }
        [BindProperty]
        public int categoryID { get; set; }
        public void OnGet(int? pageIndex, string searchname = "", int category = 0)
        {
            if (category != categoryID) pageIndex = 1;

            string loginID = HttpContext.Request.Cookies["loginId"];
            if (loginID != null)
            {
                userlogin = context.Users.FirstOrDefault(x => x.UserId == int.Parse(loginID));
            }

            // load category
            courseCategories = context.CourseCategories.ToList();

            int pageSize = 2;
            int currentPage = pageIndex ?? 1;

            IQueryable<Course> courses;

            enrollments = context.Enrollments.Where(p => p.UserId == userlogin.UserId).ToList();

            if (context.Courses != null && enrollments != null)
            {
                if (String.IsNullOrEmpty(searchname))
                {
                    courses = from course in context.Courses
                              join enrollment in context.Enrollments
                              on course.CourseId equals enrollment.CourseId
                              where enrollment.UserId == userlogin.UserId
                              select course;
                }
                else
                {
                    courses = from course in context.Courses
                              join enroll in enrollments
                              on course.CourseId equals enroll.CourseId
                              where course.Name.ToLower().Contains(searchname.ToLower())
                              select course;
                }

                if (category > 0)
                {
                    courses = courses.Include(p => p.Category)
                        .Where(p => p.CategoryId == category);
                }

                results = courses.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                searchValue = searchname;
                categoryID = category;
                ViewData["pageIndex"] = currentPage;
                ViewData["totalPages"] = Math.Ceiling((double)courses.ToList().Count / pageSize);
            }
        }

        public IActionResult OnPostSearchCourse(string searchname)
        {
            return RedirectToPage("SearchCourse", new { searchName = searchname });
        }
    }
}
