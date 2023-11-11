using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.IO;

namespace Project.Pages.Week
{
    public class AddSubmissionModel : PageModel
    {
        public User userlogin { get; set; }
        public Course c{ get; set; }
        //public Models.Enrollment e { get; set; }
        public Assignment ass { get; set; }
        public WeekLesson wl { get; set; }
        public List<Submission> smList { get; set; }
        public Submission sm { get; set; }
        public void OnGet(int id)
        {
            //assignId
            if (id != 0)
            {
                using (prn231_finalprojectContext context = new prn231_finalprojectContext())
                {
                    string loginID = HttpContext.Request.Cookies["loginId"];
                    ass = context.Assignments.FirstOrDefault(x => x.Id == id);
                    wl = context.WeekLessons.FirstOrDefault(x => x.Id == ass.Wlid);
                    c = context.Courses.FirstOrDefault(x => x.CourseId == wl.CourseId);
                    sm = context.Submissions.FirstOrDefault(x => x.AssignId == id && x.UserId == int.Parse(loginID));
                }
            }
        }
        public void uploadFile(IFormFile file, string assId)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                if (file != null && file.Length > 0 && assId!=null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);

                        // Lưu dữ liệu file vào cơ sở dữ liệu
                        var curAssign = context.Assignments.FirstOrDefault(x=>x.Id==int.Parse(assId));
                        curAssign.Attachment = memoryStream.ToArray();
                        context.SaveChanges();
                        ViewData["mess"] = "Upload file successfully!";
                        
                    }
                }
                ViewData["mess"] = "Upload file Fail!";
               
            }
        }
    }
}
