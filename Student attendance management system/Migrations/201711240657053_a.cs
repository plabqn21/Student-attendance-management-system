namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "Batch", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendances", "Batch");
        }
    }
}
