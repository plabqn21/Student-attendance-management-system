using System.ComponentModel.DataAnnotations;

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