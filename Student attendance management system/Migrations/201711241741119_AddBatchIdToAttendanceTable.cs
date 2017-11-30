namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBatchIdToAttendanceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "BatchId", c => c.Int(nullable: false));
            CreateIndex("dbo.Attendances", "BatchId");
            AddForeignKey("dbo.Attendances", "BatchId", "dbo.Batches", "Id", cascadeDelete: true);
            DropColumn("dbo.Attendances", "Batch");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attendances", "Batch", c => c.String(nullable: false));
            DropForeignKey("dbo.Attendances", "BatchId", "dbo.Batches");
            DropIndex("dbo.Attendances", new[] { "BatchId" });
            DropColumn("dbo.Attendances", "BatchId");
        }
    }
}
