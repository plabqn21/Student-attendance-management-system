using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_attendance_management_system.Models
{
    public class Course
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Id { get; set; }
        [Key]
        [Column(Order = 2)]
        [Display(Name = "Course Code")]
        [Required]
        public string CourseCode { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        public string Name { get; set; }
        [Required]
        public string Semester { get; set; }
    }
}