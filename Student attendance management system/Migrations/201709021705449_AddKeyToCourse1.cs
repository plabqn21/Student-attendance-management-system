namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKeyToCourse1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Courses");
            AlterColumn("dbo.Courses", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Courses", new[] { "Id", "CourseCode" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Courses");
            AlterColumn("dbo.Courses", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Courses", new[] { "Id", "CourseCode" });
        }
    }
}
