using System;
using System.ComponentModel.DataAnnotations;

namespace Student_attendance_management_system.Models.ViewModel
{
    public class RegisterViewModel
    {



        


        [Required]
        [MaxLength(244)]

        public String Name { get; set; }

        [Required]
        [MaxLength(244)]

        public String DateOfBirth { get; set; }
        [Required]
        [MaxLength(244)]



        public String Degree { get; set; }
        [Required]
        [MaxLength(244)]

        public String Position { get; set; }
        [Required]
        [MaxLength(14)]

        [RegularExpression(@"^(?:\+?88)?01[15-9]\d{8}$", ErrorMessage = "It is Not Valid Mobile Number")]
        public String Mobile { get; set; }




        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(244)]

        public String ProfilePicture { get; set; }
        [Required]
        [MaxLength(244)]

        public String SecrectCode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}