using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Student_attendance_management_system.Models.ViewModel
{
    public class AttendanceViewModel1
    {


      
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Date { get; set; }


        [Required]
        [ForeignKey("Sessiontbl")]
        public int SessiontblId { get; set; }

        public Sessiontbl Sessiontbl { get; set; }



        [Required]
        [ForeignKey("Semester")]
        public int SemesterId { get; set; }

        public Semester Semester { get; set; }


       

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Course Course { get; set; }

       
    }
}