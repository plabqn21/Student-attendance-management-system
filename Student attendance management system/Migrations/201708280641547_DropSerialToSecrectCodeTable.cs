namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropSerialToSecrectCodeTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SecrectCodes", "Serial");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SecrectCodes", "Serial", c => c.Int(nullable: false, identity: true));
        }
    }
}
