using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_attendance_management_system.Models.ViewModel
{
    public class DetailsAttendanceWithDateViewModel
    {

        public List<DetailsAttendanceShowViewModel> finalattendanceList { get; set; }

        public DetailsAttendanceShowViewModel Dates { get; set; }
    }
}