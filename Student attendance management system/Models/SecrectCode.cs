using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_attendance_management_system.Models
{
    public class SecrectCode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [MaxLength(10, ErrorMessage = "Maximum 10 Character")]
        [MinLength(4, ErrorMessage = "Minimum 4 Character")]
        public string SecrectKey { get; set; }




    }
}