using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Pages.Courses
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
        public List<Course> results { get; set; }
        [BindProperty]
        public string searchValue { get; set; }
        [BindProperty]
        public int categoryID { get; set; }

        public void OnGet(int? pageIndex, string searchname = "", int category = 0)
        {
            string loginID = HttpContext.Request.Cookies["loginId"];
            if (loginID != null)
            {
                userlogin = context.Users.FirstOrDefault(x => x.UserId == int.Parse(loginID));
            }
            //
            if (category != categoryID) pageIndex = 1;

            // load category
            courseCategories = context.CourseCategories.ToList();

            int pageSize = 2;
            int currentPage = pageIndex ?? 1;
            IQueryable<Course> courses;

            if (context.Courses != null)
            {
                if (string.IsNullOrEmpty(searchname))
                {
                    courses = context.Courses;
                }
                else
                {
                    courses = context.Courses.Where(p => p.Name.ToLower().Contains(searchname.ToLower()));
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
    }
}
