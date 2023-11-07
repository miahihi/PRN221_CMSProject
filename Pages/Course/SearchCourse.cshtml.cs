using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages.Course
{
    public class SearchCourseModel : PageModel
    {
        private prn231_finalprojectContext context = new prn231_finalprojectContext();
        [BindProperty(SupportsGet = true)]
        public string searchName { get; set; }
        public void OnGet()
        {
            string sname = searchName.ToLower();

            ViewData["courses"] = context.Courses.Where(p => p.Name.ToLower().Contains(sname)).ToList();
        }
    }
}
