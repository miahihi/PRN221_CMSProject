using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Project.Models;

namespace Project.Pages
{
    public class IndexModel : PageModel
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

    }
}
