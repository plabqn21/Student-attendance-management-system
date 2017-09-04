namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSessionForeignkeyToStudentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "SessiontblId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "SessiontblId");
            AddForeignKey("dbo.Students", "SessiontblId", "dbo.Sessiontbls", "Id", cascadeDelete: true);
            DropColumn("dbo.Students", "Session");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Session", c => c.String(nullable: false, maxLength: 40));
            DropForeignKey("dbo.Students", "SessiontblId", "dbo.Sessiontbls");
            DropIndex("dbo.Students", new[] { "SessiontblId" });
            DropColumn("dbo.Students", "SessiontblId");
        }
    }
}
