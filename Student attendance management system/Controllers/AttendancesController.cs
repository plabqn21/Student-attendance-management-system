using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Student_attendance_management_system.Models;
using Student_attendance_management_system.Models.ViewModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Student_attendance_management_system.Controllers
{
    public class AttendancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attendances
        public ActionResult Index()
        {
            var attendances = db.Attendances.Include(a => a.ApplicationUser).Include(a => a.Course).Include(a => a.Semester).Include(a => a.Sessiontbl).Include(a => a.Status).Include(a => a.Student);
            return View(attendances.ToList());
        }

        // GET: Attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendances/Create
        public ActionResult Create()
        {

            var currentUser = User.Identity.GetUserId();

            var assignCourses = db.CourseAssignToTeachers.Where(x => x.UserId == currentUser);

            var courses = assignCourses.Select(item => item.Course).ToList();

            //ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.CourseId = new SelectList(courses, "Id", "Name");
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name");
            ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session");
            //ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name");
            //ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentId");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttendanceViewModel1 attendanceViewModel1)
        {
            if (ModelState.IsValid)
            {
                //db.Attendances.Add(attendance);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                TempData["attendanceViewModel"] = attendanceViewModel1;
                return RedirectToAction("FinalAttendence");
            }

            //ViewBag.UserId = new SelectList(db.Users, "Id", "Name", attendanceViewModel1.UserId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", attendanceViewModel1.CourseId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", attendanceViewModel1.SemesterId);
            ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session", attendanceViewModel1.SessiontblId);
            //ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", attendance.StatusId);
            //ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentId", attendance.StudentId);
            return View(attendanceViewModel1);
        }




        [HttpGet]
        public ActionResult FinalAttendence()
        {

            var receiveData = (AttendanceViewModel1)TempData["attendanceViewModel"];

            var course = db.Courses.Where(x => x.Id == receiveData.CourseId);
            var sesester = db.Semesters.Where(x => x.Id == receiveData.SemesterId);
            var sessions = db.Sessions.Where(x => x.Id == receiveData.SessiontblId);
            var userId = User.Identity.GetUserId();

            ViewBag.CourseId = new SelectList(course, "Id", "Name");
            ViewBag.SemesterId = new SelectList(sesester, "Id", "Name");
            ViewBag.SessiontblId = new SelectList(sessions, "Id", "Session");
            ViewBag.UserId = (string)userId;

            ViewBag.Date = receiveData.Date;

            var allStudents =
                db.Students.Where(
                    x => x.SemesterId == receiveData.SemesterId && x.SessiontblId == receiveData.SessiontblId).ToList();

            var finalAttendanceViewModel = new FinalAttendanceViewModel();
            finalAttendanceViewModel.Students = allStudents;


            return View(finalAttendanceViewModel);
        }






        [HttpPost]
        public ActionResult FinalAttendence(FormCollection formCollection )
        {


            List<ReceiveAttendanceListViewModel> receive=new List<ReceiveAttendanceListViewModel>();


            var StudentId = formCollection["StudentId"].Split(',').ToArray();
                 var Name =formCollection["Name"].Split(',').ToArray();
                 var CourseId =formCollection["CourseId"].Split(',').ToArray();
                 var UserId = formCollection["UserId"].Split(',').ToArray();
                 var SessiontblId = formCollection["SessiontblId"].Split(',').ToArray();
                 var SemesterId = formCollection["SemesterId"].Split(',').ToArray();
                 var Date = formCollection["Date"].Split(',').ToArray();
                 var StatusId = formCollection["StatusId"].Split(',').ToArray();



                 for (var i = 0; i < StudentId.Length; i++)
                 {
                     receive.Add(new ReceiveAttendanceListViewModel()
                     {   StudentId=StudentId[i].ToString(),
                         
                         CourseId=Convert.ToInt32(CourseId[i]),

                         UserId=UserId[i].ToString(),
                         SessiontblId=Convert.ToInt32(SessiontblId[i]),
                         SemesterId=Convert.ToInt32(SemesterId[i]),
                         Date=Date[i].ToString(),
                         StatusId=Convert.ToInt32(StatusId[i])
                       
                     });
                 }
            List<Attendance> listAttendance=new List<Attendance>();
            foreach (var attendance in receive)
            {

                listAttendance.Add(new Attendance()
                {
                 Date   = attendance.Date,
                 SessiontblId = attendance.SessiontblId,
                 SemesterId = attendance.SemesterId,
                 UserId = attendance.UserId,
                 CourseId = attendance.CourseId,
                 StudentId =db.Students.SingleOrDefault(x=>x.StudentId==attendance.StudentId).Id,
                 StatusId = attendance.StatusId


                }
                    
                    
                    );


               

            }


            foreach (var attendance in listAttendance)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();

            }





            return RedirectToAction("Index","Home");
        }
        // GET: Attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", attendance.UserId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", attendance.CourseId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", attendance.SemesterId);
            ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session", attendance.SessiontblId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", attendance.StatusId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentId", attendance.StudentId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,SessiontblId,SemesterId,UserId,CourseId,StudentId,StatusId")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", attendance.UserId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", attendance.CourseId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", attendance.SemesterId);
            ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session", attendance.SessiontblId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", attendance.StatusId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentId", attendance.StudentId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
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
