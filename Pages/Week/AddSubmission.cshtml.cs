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
        public Course c { get; set; }
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
        public void OnPostUploadFile(IFormFile? file, IFormFile? refile, string assId)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                string loginID = HttpContext.Request.Cookies["loginId"];
                //ass = context.Assignments.FirstOrDefault(x => x.Id == int.Parse(assId));
                //wl = context.WeekLessons.FirstOrDefault(x => x.Id == ass.Wlid);
                //c = context.Courses.FirstOrDefault(x => x.CourseId == wl.CourseId);
                //sm = context.Submissions.FirstOrDefault(x => x.AssignId == int.Parse(assId) && x.UserId == int.Parse(loginID));
                if (file != null && file.Length > 0 && assId != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);

                        // Lưu dữ liệu file vào cơ sở dữ liệu
                        var curAssign = context.Assignments.FirstOrDefault(x => x.Id == int.Parse(assId));
                        curAssign.Attachment = memoryStream.ToArray();


                        Submission s = new Submission();
                        s.AssignId = int.Parse(assId);
                        s.UserId = int.Parse(loginID);
                        s.LastModifiedTime = DateTime.Now;

                        context.Submissions.Add(s);

                        context.SaveChanges();
                        ViewData["mess"] = "Upload file successfully!";
                    }
                }
                else if (refile != null && refile.Length > 0 && assId != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        refile.CopyTo(memoryStream);

                        // Lưu dữ liệu file vào cơ sở dữ liệu
                        var curAssign = context.Assignments.FirstOrDefault(x => x.Id == int.Parse(assId));
                        curAssign.Attachment = memoryStream.ToArray();


                        Submission s = new Submission();
                        s.AssignId = int.Parse(assId);
                        s.UserId = int.Parse(loginID);
                        s.LastModifiedTime = DateTime.Now;

                        context.Submissions.Add(s);

                        context.SaveChanges();
                        ViewData["mess"] = "ReUpload file successfully!";
                    }
                }
                else
                {
                    ViewData["mess"] = "Upload file Fail!";
                }

                OnGet(int.Parse(assId));
                //return RedirectToPage("/Week/Detail", new { id = assId });

                //ViewData["mess"] = "Upload file Fail!";

            }
        }
        public IActionResult OnPostUnenroll(int courseId)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                string loginID = HttpContext.Request.Cookies["loginId"];
                Enrollment data = context.Enrollments.FirstOrDefault(c => c.CourseId == courseId && c.UserId == int.Parse(loginID));
                context.Enrollments.Remove(data);
                context.SaveChanges();
                return RedirectToPage("/Courses/Index");
            }
        }
    }
}
