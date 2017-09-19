namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendances", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attendances", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Attendances", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Attendances", "SessiontblId", "dbo.Sessiontbls");
            DropForeignKey("dbo.Attendances", "StudentId", "dbo.Students");
            DropIndex("dbo.Attendances", new[] { "SessiontblId" });
            DropIndex("dbo.Attendances", new[] { "SemesterId" });
            DropIndex("dbo.Attendances", new[] { "UserId" });
            DropIndex("dbo.Attendances", new[] { "CourseId" });
            DropIndex("dbo.Attendances", new[] { "StudentId" });
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Attendances");

            Sql("Insert into Status values('Present')");
            Sql("Insert into Status values('Absent')");
            Sql("Insert into Status values('On Leave')");
        }
        
        public override void Down()
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
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Status");
            CreateIndex("dbo.Attendances", "StudentId");
            CreateIndex("dbo.Attendances", "CourseId");
            CreateIndex("dbo.Attendances", "UserId");
            CreateIndex("dbo.Attendances", "SemesterId");
            CreateIndex("dbo.Attendances", "SessiontblId");
            AddForeignKey("dbo.Attendances", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Attendances", "SessiontblId", "dbo.Sessiontbls", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Attendances", "SemesterId", "dbo.Semesters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Attendances", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Attendances", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
