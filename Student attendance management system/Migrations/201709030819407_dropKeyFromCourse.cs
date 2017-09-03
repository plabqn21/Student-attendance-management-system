namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropKeyFromCourse : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Courses");
            AlterColumn("dbo.Courses", "CourseCode", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Courses", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Courses");
            AlterColumn("dbo.Courses", "CourseCode", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Courses", new[] { "Id", "CourseCode" });
        }
    }
}
