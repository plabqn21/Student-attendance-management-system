namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseAssignToTeacherTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseAssignToTeachers",
                c => new
                    {
                        CourseAssignToTeacherId = c.Int(nullable: false, identity: true),
                        SemesterId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CourseAssignToTeacherId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: false)
                .Index(t => t.SemesterId)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseAssignToTeachers", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.CourseAssignToTeachers", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseAssignToTeachers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CourseAssignToTeachers", new[] { "UserId" });
            DropIndex("dbo.CourseAssignToTeachers", new[] { "CourseId" });
            DropIndex("dbo.CourseAssignToTeachers", new[] { "SemesterId" });
            DropTable("dbo.CourseAssignToTeachers");
        }
    }
}
