namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignkeyToCourseTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "SemesterId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "SemesterId");
            AddForeignKey("dbo.Courses", "SemesterId", "dbo.Semesters", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "SemesterId", "dbo.Semesters");
            DropIndex("dbo.Courses", new[] { "SemesterId" });
            DropColumn("dbo.Courses", "SemesterId");
        }
    }
}
