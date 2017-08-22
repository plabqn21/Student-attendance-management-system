namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSomePropertiesToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 244));
            AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.String(nullable: false, maxLength: 244));
            AddColumn("dbo.AspNetUsers", "Degree", c => c.String(nullable: false, maxLength: 244));
            AddColumn("dbo.AspNetUsers", "Position", c => c.String(nullable: false, maxLength: 244));
            AddColumn("dbo.AspNetUsers", "SecrectCode", c => c.String(nullable: false, maxLength: 244));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SecrectCode");
            DropColumn("dbo.AspNetUsers", "Position");
            DropColumn("dbo.AspNetUsers", "Degree");
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
