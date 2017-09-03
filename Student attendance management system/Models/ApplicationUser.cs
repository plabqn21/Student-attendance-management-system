using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Student_attendance_management_system.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
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

            [RegularExpression(@"^(?:\+?88)?01[15-9]\d{8}$",ErrorMessage="It is Not Valid Mobile Number")]
            public String Mobile { get; set; }[Required]
            [MaxLength(244)]
            
            public String ProfilePicture { get; set; }[Required]
            [MaxLength(244)]
            
            public String SecrectCode { get; set; }














        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}