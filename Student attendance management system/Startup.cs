using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Student_attendance_management_system.Startup))]
namespace Student_attendance_management_system
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
