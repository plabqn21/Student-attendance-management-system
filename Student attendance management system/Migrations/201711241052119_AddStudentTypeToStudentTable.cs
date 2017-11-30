namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentTypeToStudentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "StudentType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "StudentType");
        }
    }
}
