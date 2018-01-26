using Microsoft.AspNet.Identity;
using Student_attendance_management_system.Models;
using Student_attendance_management_system.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Student_attendance_management_system.Controllers
{
	public class AttendanceReportController : Controller
	{
		//  GET: AttendanceReport

		private ApplicationDbContext db = new ApplicationDbContext();


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

			var batch = db.Batches.ToList();
			@ViewBag.BatchId = new SelectList(batch, "Id", "Name");
			@ViewBag.SemesterId = new SelectList(semesters, "Id", "Name");
			@ViewBag.CourseId = new SelectList(assignedCoursesToTeacher, "Id", "Name");
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
				.Where(x => x.BatchId == attendanceReportViewModel.BatchId
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
			var batch = db.Batches.ToList();

			@ViewBag.BatchId = new SelectList(batch, "Id", "Name");
			@ViewBag.SemesterId = new SelectList(semesters, "Id", "Name");
			return View();
		}

		public ActionResult SingleDayAttendance(SingleDayAttendanceViewModel singleDayAttendanceViewModel)
		{
			var Userid = User.Identity.GetUserId();

			var attendance =
				db.Attendances.Include("Course")
					.Include("Student")
					.Include("Status")
					.Include("Semester")
					.Where(x => x.BatchId == singleDayAttendanceViewModel.BatchId
								&& x.SemesterId == singleDayAttendanceViewModel.SemesterId
								&& x.CourseId == singleDayAttendanceViewModel.CourseId
								&& x.UserId == Userid
								&& x.Date == singleDayAttendanceViewModel.Date
					).ToList();


			@ViewBag.Date = singleDayAttendanceViewModel.Date;

			@ViewBag.SemesterName =
				db.Semesters.SingleOrDefault(x => x.Id == singleDayAttendanceViewModel.SemesterId).Name;

			@ViewBag.Batch = db.Batches.FirstOrDefault(x => x.Id == singleDayAttendanceViewModel.BatchId).Name;

			@ViewBag.TeacherName = db.Users.SingleOrDefault(x => x.Id == Userid).Name;

			@ViewBag.CourseName = db.Courses.SingleOrDefault(x => x.Id == singleDayAttendanceViewModel.CourseId).Name;

			return View(attendance);
		}



		public ActionResult DetailsAttendanceParameterEntry()
		{
			var semesters = db.Semesters.ToList();

			var batches = db.Batches.ToList();
			@ViewBag.BatchId = new SelectList(batches, "Id", "Name");
			@ViewBag.SemesterId = new SelectList(semesters, "Id", "Name");

			return View();
		}




		public ActionResult DetailsAttendance(DetailsAttendanceParameterViewModel detailsAttendanceParameterViewModel)
		{

			var userid = User.Identity.GetUserId();

			var attendanceList =
				db.Attendances
					.Where(x => x.BatchId == detailsAttendanceParameterViewModel.BatchId
								&& x.SemesterId == detailsAttendanceParameterViewModel.SemesterId
								&& x.CourseId == detailsAttendanceParameterViewModel.CourseId
								&& x.UserId == userid

					).ToList();


			var finalattendanceList = new List<DetailsAttendanceShowViewModel>();
            var finalDatesList = new DetailsAttendanceShowViewModel();

			string[] AllDates = new string[27000];
			int j = 0;

			foreach (var item in attendanceList)
			{
				AllDates[j] = item.Date;

				j = j + 1;
			}


			@ViewBag.SemesterName =
				db.Semesters.SingleOrDefault(x => x.Id == detailsAttendanceParameterViewModel.SemesterId).Name;

            @ViewBag.Batch = db.Batches.FirstOrDefault(x => x.Id == detailsAttendanceParameterViewModel.BatchId).Name;

			@ViewBag.TeacherName = db.Users.SingleOrDefault(x => x.Id == userid).Name;

			@ViewBag.CourseName =
				db.Courses.SingleOrDefault(x => x.Id == detailsAttendanceParameterViewModel.CourseId).Name;


			string[] allDistinctDates = new string[70];

			for (int i = 0; i < 70; i++)
			{


				allDistinctDates[i] = "";

			}
			string[] allDistinctDatesTemp = new string[70];
			allDistinctDatesTemp = AllDates.Distinct().ToArray();


			for (int i = 0; i < allDistinctDatesTemp.Length; i++)
			{
				allDistinctDates[i] = allDistinctDatesTemp[i];

				if (allDistinctDates[i] == null)
				{
					allDistinctDates[i] = "";
				}
			}

			//ViewBag.Dates = allDistinctDates;









            finalDatesList.a = allDistinctDates[0];
            finalDatesList.b = allDistinctDates[1];
            finalDatesList.c = allDistinctDates[2];
            finalDatesList.d = allDistinctDates[3];
            finalDatesList.e = allDistinctDates[4];
            finalDatesList.f = allDistinctDates[5];
            finalDatesList.g = allDistinctDates[6];
            finalDatesList.h = allDistinctDates[7];
            finalDatesList.i = allDistinctDates[8];
            finalDatesList.j = allDistinctDates[9];
            finalDatesList.k = allDistinctDates[10];
            finalDatesList.l = allDistinctDates[11];
            finalDatesList.m = allDistinctDates[12];
            finalDatesList.n = allDistinctDates[13];
            finalDatesList.o = allDistinctDates[14];
            finalDatesList.p = allDistinctDates[15];
            finalDatesList.q = allDistinctDates[16];
            finalDatesList.r = allDistinctDates[17];
            finalDatesList.s = allDistinctDates[18];
            finalDatesList.t = allDistinctDates[19];
            finalDatesList.u = allDistinctDates[20];
            finalDatesList.v = allDistinctDates[21];
            finalDatesList.w = allDistinctDates[22];
            finalDatesList.x = allDistinctDates[23];
            finalDatesList.y = allDistinctDates[24];
            finalDatesList.z = allDistinctDates[25];
            finalDatesList.zz = allDistinctDates[26];
            finalDatesList.zzz = allDistinctDates[27];
            finalDatesList.zzzz = allDistinctDates[28];
            finalDatesList.zzzzz = allDistinctDates[29];
            finalDatesList.zzzzzz = allDistinctDates[30];
            finalDatesList.zzzzzzz = allDistinctDates[31];
            finalDatesList.zzzzzzzz = allDistinctDates[32];
            finalDatesList.zzzzzzzzz = allDistinctDates[33];
            finalDatesList.zzzzzzzzzz = allDistinctDates[34];
            finalDatesList.zzzzzzzzzzz = allDistinctDates[35];
            finalDatesList.zzzzzzzzzzzz = allDistinctDates[36];
            finalDatesList.zzzzzzzzzzzzz = allDistinctDates[37];
            finalDatesList.zzzzzzzzzzzzzz = allDistinctDates[38];
            finalDatesList.zzzzzzzzzzzzzzz = allDistinctDates[39];
            finalDatesList.zzzzzzzzzzzzzzzz = allDistinctDates[40];














			SqlDataReader reader;

			string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			using (var con = new SqlConnection(cs))
			{


				var cmd = @"
		
							 

SELECT DISTINCT Date INTO #Dates
FROM Attendances where SemesterId=@SemesterId and UserId=@UserId and CourseId=@CourseId and BatchId=@BatchId



DECLARE @cols NVARCHAR(4000)
SELECT  @cols = COALESCE(@cols + ',[' + CONVERT(varchar, Date, 106)
				+ ']','[' + CONVERT(varchar, Date, 106) + ']')
FROM    #Dates


DECLARE @qry NVARCHAR(4000)
SET @qry =
'SELECT * FROM
(SELECT StudentId, StatusId , Date
FROM Attendances)emp
PIVOT (MAX(StatusId) FOR Date IN (' + @cols + ')) AS stat'

EXEC(@qry)

DROP TABLE #Dates";




				using (var command = new SqlCommand("", con))
				{




					command.CommandText = cmd;
					command.Parameters.AddWithValue("@SemesterId", detailsAttendanceParameterViewModel.SemesterId);
					command.Parameters.AddWithValue("@UserId", userid);
					command.Parameters.AddWithValue("@CourseId", detailsAttendanceParameterViewModel.CourseId);
					command.Parameters.AddWithValue("@BatchId", detailsAttendanceParameterViewModel.BatchId);
					con.Open();
					reader = command.ExecuteReader();


					if (reader.HasRows)
					{
						while (reader.Read())
						{



							string[] alist = new string[41];

							for (int i = 0; i < reader.VisibleFieldCount; i++)
							{
								var myString = reader[i].ToString();
								alist[i] = myString;
							}

							for (int a = 0; a < 41; a++)
							{
								if (alist[a] != "1" && alist[a] != "2" && alist[a] != null && alist[a] != "")
								{
									int temp = Int32.Parse(alist[a]);

									var studentId = db.Students.SingleOrDefault(x => x.Id == temp).StudentId;
									alist[a] = studentId;
								}

								if (alist[a] == "1")
								{
									alist[a] = "P";
								}

								if (alist[a] == "2")
								{
									alist[a] = "A";
								}

								if (alist[a] == null)
								{
									alist[a] = "";
								}

							}
							DetailsAttendanceShowViewModel vm = new DetailsAttendanceShowViewModel();

							vm.a = alist[0];
							vm.b = alist[1];
							vm.c = alist[2];
							vm.d = alist[3];
							vm.e = alist[4];
							vm.f = alist[5];
							vm.g = alist[6];
							vm.h = alist[7];
							vm.i = alist[8];
							vm.j = alist[9];
							vm.k = alist[10];
							vm.l = alist[11];
							vm.m = alist[12];
							vm.n = alist[13];
							vm.o = alist[14];
							vm.p = alist[15];
							vm.q = alist[16];
							vm.r = alist[17];
							vm.s = alist[18];
							vm.t = alist[19];
							vm.u = alist[20];
							vm.v = alist[21];
							vm.w = alist[22];
							vm.x = alist[23];
							vm.y = alist[24];
							vm.z = alist[25];
							vm.zz = alist[26];
							vm.zzz = alist[27];
							vm.zzzz = alist[28];
							vm.zzzzz = alist[29];
							vm.zzzzzz = alist[30];
							vm.zzzzzzz = alist[31];
							vm.zzzzzzzz = alist[32];
							vm.zzzzzzzzz = alist[33];
							vm.zzzzzzzzzz = alist[34];
							vm.zzzzzzzzzzz = alist[35];
							vm.zzzzzzzzzzzz = alist[36];
							vm.zzzzzzzzzzzzz = alist[37];
							vm.zzzzzzzzzzzzzz = alist[38];
							vm.zzzzzzzzzzzzzzz = alist[39];
							vm.zzzzzzzzzzzzzzzz = alist[40];

							finalattendanceList.Add(vm);



						}
					}




				}




			}




            var finalattendanceListWithDates = new DetailsAttendanceWithDateViewModel();

		    finalattendanceListWithDates.Dates = finalDatesList;
		    finalattendanceListWithDates.finalattendanceList = finalattendanceList;
            return View(finalattendanceListWithDates);
		}
	}
}

