namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSerialToSecrectCodeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SecrectCodes", "Serial", c => c.Int(nullable: false, identity: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SecrectCodes", "Serial");
        }
    }
}
