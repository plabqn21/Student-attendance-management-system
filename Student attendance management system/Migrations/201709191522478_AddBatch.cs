namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBatch : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendances", "SessiontblId", "dbo.Sessiontbls");
            DropIndex("dbo.Attendances", new[] { "SessiontblId" });
            AddColumn("dbo.Attendances", "Batch", c => c.String(nullable: false));
            DropColumn("dbo.Attendances", "SessiontblId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attendances", "SessiontblId", c => c.Int(nullable: false));
            DropColumn("dbo.Attendances", "Batch");
            CreateIndex("dbo.Attendances", "SessiontblId");
            AddForeignKey("dbo.Attendances", "SessiontblId", "dbo.Sessiontbls", "Id", cascadeDelete: true);
        }
    }
}
