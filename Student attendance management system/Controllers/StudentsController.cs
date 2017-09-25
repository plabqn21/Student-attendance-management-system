using Student_attendance_management_system.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Student_attendance_management_system.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Semester).Include(s => s.Sessiontbl);
            return View(students.ToList());
        }


        [HttpPost]
        public ActionResult Index(String StudentId)
        {
            if (StudentId != null)
            {
                var student = db.Students.Include(s => s.Semester).Include(s => s.Sessiontbl).Where(x => x.StudentId == StudentId || x.Batch == StudentId).ToList();

                return View(student);
            }


            return View(db.Students.ToList());
        }


        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name");
            ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentId,Name,Batch,SemesterId,SessiontblId")] Student student)
        {

            var alreadyAddedStudent = db.Students.SingleOrDefault(x => x
                                                                       .SemesterId == student.SemesterId &&

                                                                       x.StudentId == student.StudentId



                );


            if (alreadyAddedStudent != null)
            {
                ViewBag.Message = "Already Added";
                ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", student.SemesterId);
                ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session", student.SessiontblId);
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", student.SemesterId);
            ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session", student.SessiontblId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", student.SemesterId);
            ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session", student.SessiontblId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentId,Name,Batch,SessiontblId,SemesterId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", student.SemesterId);
            ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session", student.SessiontblId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
