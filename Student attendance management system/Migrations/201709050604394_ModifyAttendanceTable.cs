namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyAttendanceTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Attendance_Id", "dbo.Attendances");
            DropIndex("dbo.Students", new[] { "Attendance_Id" });
            AddColumn("dbo.Attendances", "SessiontblId", c => c.Int(nullable: false));
            AddColumn("dbo.Attendances", "SemesterId", c => c.Int(nullable: false));
            AddColumn("dbo.Attendances", "StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Attendances", "SessiontblId");
            CreateIndex("dbo.Attendances", "SemesterId");
            CreateIndex("dbo.Attendances", "StudentId");
            AddForeignKey("dbo.Attendances", "SemesterId", "dbo.Semesters", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Attendances", "SessiontblId", "dbo.Sessiontbls", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Attendances", "StudentId", "dbo.Students", "Id", cascadeDelete: false);
            DropColumn("dbo.Students", "Attendance_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Attendance_Id", c => c.Int());
            DropForeignKey("dbo.Attendances", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Attendances", "SessiontblId", "dbo.Sessiontbls");
            DropForeignKey("dbo.Attendances", "SemesterId", "dbo.Semesters");
            DropIndex("dbo.Attendances", new[] { "StudentId" });
            DropIndex("dbo.Attendances", new[] { "SemesterId" });
            DropIndex("dbo.Attendances", new[] { "SessiontblId" });
            DropColumn("dbo.Attendances", "StudentId");
            DropColumn("dbo.Attendances", "SemesterId");
            DropColumn("dbo.Attendances", "SessiontblId");
            CreateIndex("dbo.Students", "Attendance_Id");
            AddForeignKey("dbo.Students", "Attendance_Id", "dbo.Attendances", "Id");
        }
    }
}
