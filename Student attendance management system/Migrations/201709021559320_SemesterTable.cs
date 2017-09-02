namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SemesterTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "Semester");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Semester", c => c.String(nullable: false));
        }
    }
}
