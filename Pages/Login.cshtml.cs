using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostLoginUser(string email, string password)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                var loginuser = context.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Password.Equals(password));
                if (loginuser != null)
                {
                    Response.Cookies.Append("loginId", loginuser.UserId.ToString());
                    return RedirectToPage("Course");
                }
                ViewData["mess"] = "Login Fail!!!";
                return Page();
            }
        }
    }
}
