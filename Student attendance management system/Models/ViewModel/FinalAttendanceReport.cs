using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_attendance_management_system.Models.ViewModel
{
    public class FinalAttendanceReport
    {

        public string StudentId { get; set; }
        public int TotalPresent { get; set; }
        public int TotalAbsent { get; set; }
        public int TotalLeave { get; set; }

        public double Percent { get; set; }
        public double Mark { get; set; }
        public string Name { get; set; }
    }
}