namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSessionForeignkeyToStudentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Batch", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "Batch");
            AddForeignKey("dbo.Students", "Batch", "dbo.Sessiontbls", "Id", cascadeDelete: true);
            DropColumn("dbo.Students", "Session");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Session", c => c.String(nullable: false, maxLength: 40));
            DropForeignKey("dbo.Students", "Batch", "dbo.Sessiontbls");
            DropIndex("dbo.Students", new[] { "Batch" });
            DropColumn("dbo.Students", "Batch");
        }
    }
}
