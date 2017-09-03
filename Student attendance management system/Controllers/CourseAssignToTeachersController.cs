using Student_attendance_management_system.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Student_attendance_management_system.Controllers
{
    public class CourseAssignToTeachersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CourseAssignToTeachers
        public ActionResult Index()
        {
            var courseAssignToTeachers = db.CourseAssignToTeachers.Include(c => c.ApplicationUser).Include(c => c.Course).Include(c => c.Semester);
            return View(courseAssignToTeachers.ToList());
        }

        // GET: CourseAssignToTeachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssignToTeacher courseAssignToTeacher = db.CourseAssignToTeachers.Find(id);
            if (courseAssignToTeacher == null)
            {
                return HttpNotFound();
            }
            return View(courseAssignToTeacher);
        }

        // GET: CourseAssignToTeachers/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name");
            return View();
        }

        // POST: CourseAssignToTeachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseAssignToTeacherId,SemesterId,CourseId,UserId")] CourseAssignToTeacher courseAssignToTeacher)
        {
            if (ModelState.IsValid)
            {
                db.CourseAssignToTeachers.Add(courseAssignToTeacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", courseAssignToTeacher.UserId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", courseAssignToTeacher.CourseId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", courseAssignToTeacher.SemesterId);
            return View(courseAssignToTeacher);
        }

        // GET: CourseAssignToTeachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssignToTeacher courseAssignToTeacher = db.CourseAssignToTeachers.Find(id);
            if (courseAssignToTeacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", courseAssignToTeacher.UserId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", courseAssignToTeacher.CourseId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", courseAssignToTeacher.SemesterId);
            return View(courseAssignToTeacher);
        }

        // POST: CourseAssignToTeachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseAssignToTeacherId,SemesterId,CourseId,UserId")] CourseAssignToTeacher courseAssignToTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseAssignToTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", courseAssignToTeacher.UserId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", courseAssignToTeacher.CourseId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", courseAssignToTeacher.SemesterId);
            return View(courseAssignToTeacher);
        }

        // GET: CourseAssignToTeachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssignToTeacher courseAssignToTeacher = db.CourseAssignToTeachers.Find(id);
            if (courseAssignToTeacher == null)
            {
                return HttpNotFound();
            }
            return View(courseAssignToTeacher);
        }

        // POST: CourseAssignToTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseAssignToTeacher courseAssignToTeacher = db.CourseAssignToTeachers.Find(id);
            db.CourseAssignToTeachers.Remove(courseAssignToTeacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
