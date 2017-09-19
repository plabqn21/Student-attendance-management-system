using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_attendance_management_system.Models
{
    public class Attendance
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Date { get; set; }


        [Required]

        public string Batch { get; set; }





        [Required]
        [ForeignKey("Semester")]
        public int SemesterId { get; set; }

        public Semester Semester { get; set; }


        [Required]

        [ForeignKey("ApplicationUser")]


        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public Student Student { get; set; }
        [Required]
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        public Status Status { get; set; }



    }
}