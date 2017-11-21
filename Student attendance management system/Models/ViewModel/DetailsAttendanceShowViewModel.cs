using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_attendance_management_system.Models.ViewModel
{
    public class DetailsAttendanceShowViewModel
    {
        public int StudentId { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }

    }
}