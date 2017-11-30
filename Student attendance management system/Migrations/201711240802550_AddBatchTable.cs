namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBatchTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Batches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Batches");
        }
    }
}
