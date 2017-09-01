using Student_attendance_management_system.Models;
using System.Linq;
using System.Web.Mvc;

namespace Student_attendance_management_system.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private ApplicationDbContext db;

        public AdminController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult AdminHome()
        {







            return View();
        }




        public ActionResult ViewAllTeachers()
        {


            var teachers = db.Users.ToList();




            return View(teachers);
        }


    }
}