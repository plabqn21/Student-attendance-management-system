using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Student_attendance_management_system.Models
{
    public class Semester
    {

       
       
        public int Id { get; set; }
        [Required]
        [Display(Name = "Semester Name")]
        public string Name { get; set; }
    }
}