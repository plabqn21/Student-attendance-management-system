﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_attendance_management_system.Models
{
    public class Student
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        [Display(Name = "Student ID")]
        public string StudentId { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Sessiontbl")]

        public int SessiontblId { get; set; }
        public Sessiontbl Sessiontbl { get; set; }

        [Required]
        [ForeignKey("Semester")]
        public int SemesterId { get; set; }

        public Semester Semester { get; set; }

    }
}