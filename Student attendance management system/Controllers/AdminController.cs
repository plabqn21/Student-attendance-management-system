using System.Web.Mvc;

namespace Student_attendance_management_system.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminHome()
        {
            return View();
        }
    }
}