﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_attendance_management_system.Models.ViewModel
{
    public class AttendanceViewModel1
    {



        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Date { get; set; }



        [Required]
        [ForeignKey("Batch")]
        public int BatchId { get; set; }

        public Batch Batch { get; set; }





        [Required]
        [ForeignKey("Semester")]
        [Display(Name = "Semester")]
        public int SemesterId { get; set; }

        public Semester Semester { get; set; }




        [Required]
        [ForeignKey("Course")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        public Course Course { get; set; }


    }
}