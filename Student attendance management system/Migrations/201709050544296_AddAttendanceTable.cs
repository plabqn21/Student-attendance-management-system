namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendanceTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(nullable: false, maxLength: 20),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.Int(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            AddColumn("dbo.Students", "Attendance_Id", c => c.Int());
            CreateIndex("dbo.Students", "Attendance_Id");
            AddForeignKey("dbo.Students", "Attendance_Id", "dbo.Attendances", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Attendance_Id", "dbo.Attendances");
            DropForeignKey("dbo.Attendances", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Attendances", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Students", new[] { "Attendance_Id" });
            DropIndex("dbo.Attendances", new[] { "CourseId" });
            DropIndex("dbo.Attendances", new[] { "UserId" });
            DropColumn("dbo.Students", "Attendance_Id");
            DropTable("dbo.Attendances");
        }
    }
}
