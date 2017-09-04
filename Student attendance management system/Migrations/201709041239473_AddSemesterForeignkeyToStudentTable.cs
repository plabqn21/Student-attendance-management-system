namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSemesterForeignkeyToStudentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "SemesterId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "SemesterId");
            AddForeignKey("dbo.Students", "SemesterId", "dbo.Semesters", "Id", cascadeDelete: true);
            DropColumn("dbo.Students", "Semester");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Semester", c => c.String(nullable: false, maxLength: 40));
            DropForeignKey("dbo.Students", "SemesterId", "dbo.Semesters");
            DropIndex("dbo.Students", new[] { "SemesterId" });
            DropColumn("dbo.Students", "SemesterId");
        }
    }
}
