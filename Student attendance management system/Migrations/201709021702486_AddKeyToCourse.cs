namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKeyToCourse : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Courses");
            AddPrimaryKey("dbo.Courses", new[] { "Id", "CourseCode" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Courses");
            AddPrimaryKey("dbo.Courses", "CourseCode");
        }
    }
}
