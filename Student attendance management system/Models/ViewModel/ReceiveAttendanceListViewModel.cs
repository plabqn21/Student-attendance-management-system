﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_attendance_management_system.Models.ViewModel
{
    public class ReceiveAttendanceListViewModel
    {

        public string StudentId { get; set; }

        public string Name { get; set; }


        public int CourseId { get; set; }

        public string UserId { get; set; }


        public string Batch { get; set; }
        public int SemesterId { get; set; }

        public string Date { get; set; }

        public int StatusId { get; set; }


    }
}