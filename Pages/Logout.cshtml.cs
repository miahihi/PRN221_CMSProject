using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.Pages
{
    public class LogoutModel : PageModel
    {
        public RedirectToPageResult OnGet()
        {
            Response.Cookies.Delete("loginId");
            return RedirectToPage("Index");
        }
    }
}
