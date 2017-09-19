using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Student_attendance_management_system.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Sessiontbl> Sessions { get; set; }
        public DbSet<SecrectCode> SecrectCodes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<CourseAssignToTeacher> CourseAssignToTeachers { get; set; }



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