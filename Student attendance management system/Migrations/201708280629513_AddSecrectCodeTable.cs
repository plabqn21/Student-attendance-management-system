namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSecrectCodeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SecrectCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SecrectKey = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SecrectCodes");
        }
    }
}
