using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;
using System.Net.Mail;

namespace Project.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostLoginUser(string email, string password)
        {
            if(String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            {
                ViewData["mess"] = "You have to fill email and password!";
                return Page();
            }

            if (!validate(email))
            {
                ViewData["mess"] = "Your email is wrong!";
                return Page();
            }

            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                var loginuser = context.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Password.Equals(password));
                if (loginuser != null)
                {
                    Response.Cookies.Append("loginId", loginuser.UserId.ToString());
                    return RedirectToPage("/Courses/Index");
                }
                ViewData["mess"] = "Login Fail!!!";
                return Page();
            }
        }
        private bool validate(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            } catch
            {
                return false;
            }
        }
    }
}
