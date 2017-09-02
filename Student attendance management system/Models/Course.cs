using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_attendance_management_system.Models
{
    public class Course
    {
        
             [Key]
        [Column(Order  =1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        [Column(Order = 2)]
        [Display(Name = "Course Code")]
        [Required]
        public string CourseCode { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        public string Name { get; set; }

        public Semester Semester { get; set; }
        [Required]
        [ForeignKey("Semester")]
        public int SemesterId { get; set; }
       
      
    }
}