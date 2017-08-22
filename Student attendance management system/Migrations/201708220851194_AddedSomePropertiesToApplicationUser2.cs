namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSomePropertiesToApplicationUser2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Mobile", c => c.String(nullable: false, maxLength: 244));
            AddColumn("dbo.AspNetUsers", "ProfilePicture", c => c.String(nullable: false, maxLength: 244));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ProfilePicture");
            DropColumn("dbo.AspNetUsers", "Mobile");
        }
    }
}
