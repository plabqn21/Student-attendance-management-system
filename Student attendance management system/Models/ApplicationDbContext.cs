using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Student_attendance_management_system.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {



        public DbSet<SecrectCode> SecrectCodes { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}