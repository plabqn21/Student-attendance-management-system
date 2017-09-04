namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessiontbls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Session = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sessiontbls");
        }
    }
}
