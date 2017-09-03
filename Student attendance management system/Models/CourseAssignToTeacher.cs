using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_attendance_management_system.Models
{
    public class CourseAssignToTeacher
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int CourseAssignToTeacherId { get; set; }


        [Required]
        [ForeignKey("Semester")]
        public int SemesterId { get; set; }
        
        public Semester Semester { get; set; }
        [Required]


        [ForeignKey("Course")]
        public int CourseId { get; set; }





        public Course Course { get; set; }
        [Required]

        [ForeignKey("ApplicationUser")]
        
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }










    }
}