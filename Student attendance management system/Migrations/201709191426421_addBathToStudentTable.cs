namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBathToStudentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Batch", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Batch");
        }
    }
}
