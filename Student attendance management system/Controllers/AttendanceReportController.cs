using Microsoft.AspNet.Identity;
using Student_attendance_management_system.Models;
using Student_attendance_management_system.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Student_attendance_management_system.Controllers
{
    public class AttendanceReportController : Controller
    {
        // GET: AttendanceReport

        ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public ActionResult SelectProperties()
        {

            var semesters = db.Semesters.ToList();
            var currentUser = User.Identity.GetUserId();
            var courseAssignToTeachers = db.CourseAssignToTeachers.Where(x => x.UserId == currentUser);
            var assignedCoursesToTeacher = courseAssignToTeachers.Select(x => x.Course).ToList();
            @ViewBag.SemesterId = new SelectList(semesters, "Id", "Name");
            @ViewBag.CourseId = new SelectList(assignedCoursesToTeacher, "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult SelectProperties(AttendanceReportViewModel attendanceReportViewModel)
        {


            return RedirectToAction("Index", "AttendanceReport", attendanceReportViewModel);

        }
        public ActionResult Index(AttendanceReportViewModel attendanceReportViewModel)
        {

            int totalClass = 0, totalPresent = 0, totalAbsent = 0, totalLeave = 0;
            double percent;
            var userId = User.Identity.GetUserId();

            var allStudentsAttendanceList = db.Attendances
                .Where(x => x.Batch == attendanceReportViewModel.Batch
                            && x.SemesterId == attendanceReportViewModel.SemesterId
                            && x.CourseId == attendanceReportViewModel.CourseId
                            && x.UserId == userId

                ).ToList();

            var finalAttendanceReports = new List<FinalAttendanceReport>();
            List<int> countedStudentId=new List<int>();

            foreach (var student in allStudentsAttendanceList)
            {


                var studentId =countedStudentId.Contains(student.StudentId);
                    if (studentId==false)
                    {
                        
                   
               
                foreach (var searchStudent in allStudentsAttendanceList)
                {  

                    if (student.StudentId == searchStudent.StudentId)
                    {
                        totalClass = totalClass + 1;

                        if (searchStudent.StatusId == 1)
                        {
                            totalPresent = totalPresent + 1;
                        }
                        else if (searchStudent.StatusId == 2)
                        {
                            totalAbsent = totalAbsent + 1;
                        }

                        else
                        {
                            totalLeave = totalLeave + 1;
                        }

                    }
                }


                var singleOrDefault = db.Students.SingleOrDefault(x => x.Id == student.StudentId);
                    if (singleOrDefault != null)
                        finalAttendanceReports.Add(new FinalAttendanceReport
                        {
                            StudentId = singleOrDefault.StudentId,
                            TotalPresent = totalPresent,
                            TotalAbsent = totalAbsent,
                            TotalLeave = totalLeave
                        });

                    totalPresent = 0;
                totalAbsent = 0;
                totalLeave = 0;
               countedStudentId.Add(student.StudentId);
                    }


            }
            
            return View(finalAttendanceReports);
        }
    }
}