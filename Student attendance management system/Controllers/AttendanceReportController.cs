﻿using System;
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


        public ActionResult GetAllCourses(int SemesterId)
        {
            var currentUser = User.Identity.GetUserId();
            var assignedCourse = db.CourseAssignToTeachers.Where(x => x.UserId == currentUser);


            var CourseList = assignedCourse.Select(x => x.Course).Where(x => x.SemesterId == SemesterId).ToList();

            ViewBag.SendIdToPartial = new SelectList(CourseList, "Id", "Name");
            return PartialView("CascadingOptionPartial");
        }
        [HttpGet]
        public ActionResult SelectProperties()
        {

            var semesters = db.Semesters.ToList();
            var currentUser = User.Identity.GetUserId();
            var courseAssignToTeachers = db.CourseAssignToTeachers.Where(x => x.UserId == currentUser);
            var assignedCoursesToTeacher = courseAssignToTeachers.Select(x => x.Course).ToList();
            @ViewBag.SemesterId = new SelectList(semesters, "Id", "Name");
            //@ViewBag.CourseId = new SelectList(assignedCoursesToTeacher, "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult SelectProperties(AttendanceReportViewModel attendanceReportViewModel)
        {


            return RedirectToAction("Index", "AttendanceReport", attendanceReportViewModel);

        }
        [HttpGet]
        public ActionResult Index(AttendanceReportViewModel attendanceReportViewModel)
        {

            int totalPresent = 0, totalAbsent = 0, totalLeave = 0;

            double totalClass = 0.00;
            var userId = User.Identity.GetUserId();

            var allStudentsAttendanceList = db.Attendances
                .Where(x => x.Batch == attendanceReportViewModel.Batch
                            && x.SemesterId == attendanceReportViewModel.SemesterId
                            && x.CourseId == attendanceReportViewModel.CourseId
                            && x.UserId == userId

                ).ToList();

            var finalAttendanceReports = new List<FinalAttendanceReport>();
            List<int> countedStudentId = new List<int>();

            foreach (var student in allStudentsAttendanceList)
            {


                var studentId = countedStudentId.Contains(student.StudentId);
                if (studentId == false)
                {



                    foreach (var searchStudent in allStudentsAttendanceList)
                    {

                        if (student.StudentId == searchStudent.StudentId)
                        {


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
                    if (totalClass == 0.00)
                    {
                        totalClass = (double)totalPresent + (double)totalAbsent + (double)totalLeave;
                    }
                    double parcent = Math.Round((totalPresent / totalClass) * 100.00, 2); 
                    double mark;
                    if (parcent < 60)
                    {
                        mark = 0.00;
                    }
                    else if (parcent >= 60 && parcent < 70)
                    {
                        mark = 6.00;
                    }

                    else if (parcent >= 70 && parcent < 80)
                    {
                        mark = 7.00;
                    }
                    else if (parcent >= 80 && parcent < 90)
                    {
                        mark = 8.00;
                    }
                    else
                    {
                        mark = 10.00;
                    }


                    var singleOrDefault = db.Students.SingleOrDefault(x => x.Id == student.StudentId);
                    var StudentName = singleOrDefault.Name;
                    if (singleOrDefault != null)
                        finalAttendanceReports.Add(new FinalAttendanceReport
                        {
                            StudentId = singleOrDefault.StudentId,
                            TotalPresent = totalPresent,
                            TotalAbsent = totalAbsent,
                            TotalLeave = totalLeave,
                            Percent = parcent,
                            Mark = mark,
                            Name = StudentName




                        });

                    totalPresent = 0;
                    totalAbsent = 0;
                    totalLeave = 0;

                    countedStudentId.Add(student.StudentId);
                }


            }

            ViewBag.Message = totalClass;
            var user = User.Identity.GetUserId();
            var applicationUser = db.Users.SingleOrDefault(x => x.Id == user);
            if (applicationUser != null)
                ViewBag.TeacherName = applicationUser.Name;
            var orDefault = db.Semesters.SingleOrDefault(x => x.Id == attendanceReportViewModel.SemesterId);
            if (orDefault != null)
                ViewBag.SemesterName = orDefault.Name;
            ViewBag.Batch = attendanceReportViewModel.Batch;
            var @default = db.Courses.SingleOrDefault(x => x.Id == attendanceReportViewModel.CourseId);
            if (@default != null)
                ViewBag.CourseName = @default.Name;







            return View(finalAttendanceReports);
        }



        public ActionResult DetailsAttendanceForADay()
        {

            var semesters = db.Semesters.ToList();


            @ViewBag.SemesterId = new SelectList(semesters, "Id", "Name");
            return View();
        }

        public ActionResult SingleDayAttendance(SingleDayAttendanceViewModel singleDayAttendanceViewModel)
        {
            var Userid = User.Identity.GetUserId();

            var attendance = db.Attendances.Include("Course").Include("Student").Include("Status").Include("Semester").Where(x => x.Batch == singleDayAttendanceViewModel.Batch
                                                       && x.SemesterId == singleDayAttendanceViewModel.SemesterId
                                                       && x.CourseId == singleDayAttendanceViewModel.CourseId
                                                       && x.UserId == Userid
                                                       && x.Date == singleDayAttendanceViewModel.Date
                ).ToList();


            @ViewBag.Date = singleDayAttendanceViewModel.Date;

            @ViewBag.SemesterName =
                db.Semesters.SingleOrDefault(x => x.Id == singleDayAttendanceViewModel.SemesterId).Name;

            @ViewBag.Batch = singleDayAttendanceViewModel.Batch;

            @ViewBag.TeacherName = db.Users.SingleOrDefault(x => x.Id == Userid).Name;

            @ViewBag.CourseName = db.Courses.SingleOrDefault(x => x.Id == singleDayAttendanceViewModel.CourseId).Name;

            return View(attendance);
        }
    }
}

