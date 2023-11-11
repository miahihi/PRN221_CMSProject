using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages.Week
{
    public class IndexModel : PageModel
    {
        public User userlogin { get; set; }
        public Models.Course c { get; set; }
        //public Models.Enrollment e { get; set; }
        public List<Assignment> ass { get; set; }
        public WeekLesson wl { get; set; }
        public WeekLesson preWeek {  get; set; }
        public WeekLesson nextWeek {  get; set; }

        public void OnGet(int id)
        {
            //id: weekid
            if (id != 0)
            {
                using (prn231_finalprojectContext context = new prn231_finalprojectContext())
                {
                    string loginID = HttpContext.Request.Cookies["loginId"];

                    userlogin = context.Users.FirstOrDefault(x => x.UserId == int.Parse(loginID));
                    wl = context.WeekLessons.FirstOrDefault(x => x.Id == id);
                    c = context.Courses.FirstOrDefault(x => x.CourseId == wl.CourseId);
                    ass = context.Assignments
                        .Where(x => x.Wlid == id).ToList();


                    List<WeekLesson> weekLessons = context.WeekLessons.Where(p => p.CourseId == c.CourseId).OrderBy(p=> p.StartDate).ToList();
                    if(weekLessons.Count > 1)
                    {
                        int i;
                        for(i=0; i<weekLessons.Count; i++)
                        {
                            if (weekLessons[i].StartDate == wl.StartDate) break;
                        }
                        if (i > 0)
                        {
                            preWeek = weekLessons[i - 1];
                        } else
                        {
                            preWeek = null;
                        }

                        if (i < weekLessons.Count-1)
                        {
                            nextWeek = weekLessons[i + 1];
                        }
                        else
                        {
                            nextWeek = null;
                        }
                    } else
                    {
                        preWeek = null;
                        nextWeek = null;
                    }
                }
            }
        }
    }
}
