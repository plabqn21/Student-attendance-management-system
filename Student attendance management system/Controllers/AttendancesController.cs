using Microsoft.AspNet.Identity;
using Student_attendance_management_system.Models;
using Student_attendance_management_system.Models.ViewModel;
using System;
using System.Collections.Generic;
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




            var selectedPropertiesForLoadAttendance = (SingleDayAttendanceViewModel)TempData["singleDayAttendanceViewModel"];
            var teacherId = User.Identity.GetUserId();

            var attendanceList = db.Attendances.Include(a => a.ApplicationUser).Include(a => a.Course).Include(a => a.Semester).Include(a => a.Status).Include(a => a.Student).Where(x => x.UserId == teacherId
                                                           && x.BatchId == selectedPropertiesForLoadAttendance.BatchId
                                                           &&
                                                           x.SemesterId ==
                                                           selectedPropertiesForLoadAttendance.SemesterId
                                                           && x.Date == selectedPropertiesForLoadAttendance.Date
                                                           && x.CourseId == selectedPropertiesForLoadAttendance.CourseId




                );




            TempData["singleDayAttendanceViewModel"] = selectedPropertiesForLoadAttendance;



            //  var attendances = db.Attendances.Include(a => a.ApplicationUser).Include(a => a.Course).Include(a => a.Semester).Include(a => a.Status).Include(a => a.Student);
            return View(attendanceList.ToList());
        }



        public ActionResult GetAllCourses(int SemesterId)
        {
            var currentUser = User.Identity.GetUserId();
            var assignedCourse = db.CourseAssignToTeachers.Where(x => x.UserId == currentUser);


            var CourseList = assignedCourse.Select(x => x.Course).Where(x => x.SemesterId == SemesterId);

            ViewBag.SendIdToPartial = new SelectList(CourseList, "Id", "Name");
            return PartialView("CascadingOptionPartial");
        }


        // GET: Attendances/Details/5



        // GET: Attendances/Create
        public ActionResult Create()
        {

            var currentUser = User.Identity.GetUserId();

            var assignCourses = db.CourseAssignToTeachers.Where(x => x.UserId == currentUser);

            var courses = assignCourses.Select(item => item.Course).ToList();

            //ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            // ViewBag.CourseId = new SelectList(courses, "Id", "Name");
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name");
            ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session");
            ViewBag.BatchId = new SelectList(db.Batches, "Id", "Name");
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

            var Userid = User.Identity.GetUserId();

            var attendance = db.Attendances.FirstOrDefault(x => x.BatchId == attendanceViewModel1.BatchId
                                                     && x.SemesterId == attendanceViewModel1.SemesterId
                                                     && x.CourseId == attendanceViewModel1.CourseId
                                                     && x.UserId == Userid
                                                     && x.Date == attendanceViewModel1.Date
              );

            if (attendance != null)
            {
                @ViewBag.AttendanceExist = "Already taken attendance for this date";

                //ViewBag.UserId = new SelectList(db.Users, "Id", "Name", attendanceViewModel1.UserId);
                ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", attendanceViewModel1.CourseId);
                ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", attendanceViewModel1.SemesterId);
                ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session", attendanceViewModel1.Batch);
                ViewBag.BatchId = new SelectList(db.Batches, "Id", "Name");
                //ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", attendance.StatusId);
                //ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentId", attendance.StudentId);
                return View(attendanceViewModel1);


            }


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
            ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session", attendanceViewModel1.Batch);
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
            var batch = db.Batches.Where(x => x.Id == receiveData.BatchId);
            var userId = User.Identity.GetUserId();

            ViewBag.CourseId = new SelectList(course, "Id", "Name");
            ViewBag.SemesterId = new SelectList(sesester, "Id", "Name");
            ViewBag.BatchId = new SelectList(batch, "Id", "Name");
            ViewBag.UserId = (string)userId;

            ViewBag.Date = receiveData.Date;


            var allStudents =
                db.Students.Where(
                    x => x.BatchId == receiveData.BatchId && x.StudentType == "Active").ToList();

            var finalAttendanceViewModel = new FinalAttendanceViewModel();
            finalAttendanceViewModel.Students = allStudents;


            var singleOrDefault = db.Semesters.SingleOrDefault(x => x.Id == receiveData.SemesterId);
            if (singleOrDefault != null)
                @ViewBag.SemesterName =
                    singleOrDefault.Name;

            var userid = User.Identity.GetUserId();

            var applicationUser = db.Users.SingleOrDefault(x => x.Id == userid);
            if (applicationUser != null)
                @ViewBag.TeacherName = applicationUser.Name;

            var orDefault = db.Courses.SingleOrDefault(x => x.Id == receiveData.CourseId);
            if (orDefault != null)
                @ViewBag.CourseName = orDefault.Name;

            return View(finalAttendanceViewModel);
        }






        [HttpPost]
        public ActionResult FinalAttendence(FormCollection formCollection)
        {





            var receive = new List<ReceiveAttendanceListViewModel>();


            var StudentId = formCollection["StudentId"].Split(',').ToArray();
            var Name = formCollection["Name"].Split(',').ToArray();
            var CourseId = formCollection["CourseId"].Split(',').ToArray();
            var UserId = formCollection["UserId"].Split(',').ToArray();
            var BatchId = formCollection["BatchId"].Split(',').ToArray();
            var SemesterId = formCollection["SemesterId"].Split(',').ToArray();
            var Date = formCollection["Date"].Split(',').ToArray();
            var StatusId = formCollection["StatusId"].Split(',').ToArray();







            for (var i = 0; i < StudentId.Length; i++)
            {
                receive.Add(new ReceiveAttendanceListViewModel()
                {
                    StudentId = StudentId[i].ToString(),

                    CourseId = Convert.ToInt32(CourseId[i]),

                    UserId = UserId[i].ToString(),
                    BatchId = Convert.ToInt32(BatchId[i]),
                    SemesterId = Convert.ToInt32(SemesterId[i]),
                    Date = Date[i].ToString(),
                    StatusId = Convert.ToInt32(StatusId[i])

                });
            }
            var listAttendance = new List<Attendance>();
            foreach (var attendance in receive)
            {
                var singleOrDefault = db.Students.FirstOrDefault(x => x.StudentId == attendance.StudentId && x.StudentType == "Active");
                if (singleOrDefault != null)
                    listAttendance.Add(new Attendance()
                    {
                        Date = attendance.Date,
                        BatchId = attendance.BatchId,
                        SemesterId = attendance.SemesterId,
                        UserId = attendance.UserId,
                        CourseId = attendance.CourseId,
                        StudentId = singleOrDefault.Id,
                        StatusId = attendance.StatusId


                    }


                        );
            }


            foreach (var attendance in listAttendance)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();

            }




            return RedirectToAction("Successful", "Attendances");
        }
        //  GET: Attendances/Edit/5

        [HttpGet]
        public ActionResult EditParameterEntry()
        {

            var semesters = db.Semesters.ToList();
            var currentUser = User.Identity.GetUserId();
            var Batch = db.Batches.ToList();
            var courseAssignToTeachers = db.CourseAssignToTeachers.Where(x => x.UserId == currentUser);
            var assignedCoursesToTeacher = courseAssignToTeachers.Select(x => x.Course).ToList();
            @ViewBag.SemesterId = new SelectList(semesters, "Id", "Name");
            @ViewBag.CourseId = new SelectList(assignedCoursesToTeacher, "Id", "Name");
            @ViewBag.BatchId = new SelectList(Batch, "Id", "Name");


            return View();
        }


        [HttpPost]
        public ActionResult EditParameterEntry(SingleDayAttendanceViewModel singleDayAttendanceViewModel)
        {

            TempData["singleDayAttendanceViewModel"] = singleDayAttendanceViewModel;

            return RedirectToAction("Index");

        }
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
            ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session", attendance.Batch);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", attendance.StatusId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentId", attendance.StudentId);
            ViewBag.BatchId = new SelectList(db.Batches, "Id", "Name", attendance.BatchId);
            var selectedPropertiesForLoadAttendance = (SingleDayAttendanceViewModel)TempData["singleDayAttendanceViewModel"];
            TempData["singleDayAttendanceViewModel"] = selectedPropertiesForLoadAttendance;
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,BatchId,SemesterId,UserId,CourseId,StudentId,StatusId")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                var selectedPropertiesForLoadAttendance = (SingleDayAttendanceViewModel)TempData["singleDayAttendanceViewModel"];
                TempData["singleDayAttendanceViewModel"] = selectedPropertiesForLoadAttendance;
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", attendance.UserId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", attendance.CourseId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", attendance.SemesterId);
            ViewBag.SessiontblId = new SelectList(db.Sessions, "Id", "Session", attendance.Batch);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", attendance.StatusId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentId", attendance.StudentId);
            ViewBag.BatchId = new SelectList(db.Batches, "Id", "Name", attendance.BatchId);
            return View();
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

        public ActionResult Successful()
        {
            return View();
        }
    }
}
