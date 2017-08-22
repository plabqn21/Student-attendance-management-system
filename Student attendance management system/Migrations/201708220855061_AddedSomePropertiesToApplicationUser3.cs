namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSomePropertiesToApplicationUser3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Mobile", c => c.String(nullable: false, maxLength: 14));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Mobile", c => c.String(nullable: false, maxLength: 244));
        }
    }
}
