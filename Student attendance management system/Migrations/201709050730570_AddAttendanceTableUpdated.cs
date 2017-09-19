namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendanceTableUpdated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(nullable: false, maxLength: 20),
                        SessiontblId = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: false)
                .ForeignKey("dbo.Sessiontbls", t => t.SessiontblId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: false)
                .Index(t => t.SessiontblId)
                .Index(t => t.SemesterId)
                .Index(t => t.UserId)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId)
                .Index(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Attendances", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Attendances", "SessiontblId", "dbo.Sessiontbls");
            DropForeignKey("dbo.Attendances", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Attendances", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Attendances", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "StatusId" });
            DropIndex("dbo.Attendances", new[] { "StudentId" });
            DropIndex("dbo.Attendances", new[] { "CourseId" });
            DropIndex("dbo.Attendances", new[] { "UserId" });
            DropIndex("dbo.Attendances", new[] { "SemesterId" });
            DropIndex("dbo.Attendances", new[] { "SessiontblId" });
            DropTable("dbo.Attendances");
        }
    }
}
