namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyStudentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Students", "SessiontblId", "dbo.Sessiontbls");
            DropIndex("dbo.Students", new[] { "SessiontblId" });
            DropIndex("dbo.Students", new[] { "SemesterId" });
            AddColumn("dbo.Students", "BatchId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "BatchId");
            AddForeignKey("dbo.Students", "BatchId", "dbo.Batches", "Id", cascadeDelete: true);
            DropColumn("dbo.Students", "SessiontblId");
            DropColumn("dbo.Students", "SemesterId");
            DropColumn("dbo.Students", "Batch");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Batch", c => c.String(nullable: false));
            AddColumn("dbo.Students", "SemesterId", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "SessiontblId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Students", "BatchId", "dbo.Batches");
            DropIndex("dbo.Students", new[] { "BatchId" });
            DropColumn("dbo.Students", "BatchId");
            CreateIndex("dbo.Students", "SemesterId");
            CreateIndex("dbo.Students", "SessiontblId");
            AddForeignKey("dbo.Students", "SessiontblId", "dbo.Sessiontbls", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Students", "SemesterId", "dbo.Semesters", "Id", cascadeDelete: true);
        }
    }
}
