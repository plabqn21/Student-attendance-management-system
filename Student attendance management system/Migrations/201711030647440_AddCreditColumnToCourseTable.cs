namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreditColumnToCourseTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Credit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Credit");
        }
    }
}
