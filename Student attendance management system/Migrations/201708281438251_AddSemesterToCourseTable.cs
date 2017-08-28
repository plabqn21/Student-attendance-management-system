namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSemesterToCourseTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Semester", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Semester");
        }
    }
}
