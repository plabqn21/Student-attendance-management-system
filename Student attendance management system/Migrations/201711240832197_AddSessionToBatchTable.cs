namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSessionToBatchTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Batches", "Session", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Batches", "Session");
        }
    }
}
