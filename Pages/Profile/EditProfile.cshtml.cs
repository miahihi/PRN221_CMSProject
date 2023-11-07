using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages.Profile
{
    public class EditProfileModel : PageModel
    {
        public User userlogin { get; set; }

        public void OnGet()
        {
            string loginID = HttpContext.Request.Cookies["loginId"];
            if (loginID != null)
            {
                using (prn231_finalprojectContext context = new prn231_finalprojectContext())
                {
                    userlogin = context.Users.FirstOrDefault(x => x.UserId == int.Parse(loginID));
                }
            }
        }
        public IActionResult OnPostUpdateProfile(string fullname)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                string loginID = HttpContext.Request.Cookies["loginId"];
                userlogin = context.Users.FirstOrDefault(x => x.UserId == int.Parse(loginID));
                userlogin.Fullname = fullname;
                context.SaveChanges();
                return Page();
            }
        }
    }
}
