namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBack : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SchedTypeSchedule", "SchedType_ID", "dbo.SchedType");
            DropForeignKey("dbo.SchedTypeSchedule", "Schedule_ID", "dbo.Schedule");
            DropForeignKey("dbo.FileCatFileSubCat", "FileCat_ID", "dbo.FileCat");
            DropForeignKey("dbo.FileCatFileSubCat", "FileSubCat_ID", "dbo.FileSubCat");
            DropIndex("dbo.SchedTypeSchedule", new[] { "SchedType_ID" });
            DropIndex("dbo.SchedTypeSchedule", new[] { "Schedule_ID" });
            DropIndex("dbo.FileCatFileSubCat", new[] { "FileCat_ID" });
            DropIndex("dbo.FileCatFileSubCat", new[] { "FileSubCat_ID" });
            AddColumn("dbo.Schedule", "SchedType_ID", c => c.Int());
            AddColumn("dbo.FileSubCat", "FlCat_ID", c => c.Int());
            CreateIndex("dbo.Schedule", "SchedType_ID");
            CreateIndex("dbo.FileSubCat", "FlCat_ID");
            AddForeignKey("dbo.Schedule", "SchedType_ID", "dbo.SchedType", "ID");
            AddForeignKey("dbo.FileSubCat", "FlCat_ID", "dbo.FileCat", "ID");
            DropTable("dbo.SchedTypeSchedule");
            DropTable("dbo.FileCatFileSubCat");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FileCatFileSubCat",
                c => new
                    {
                        FileCat_ID = c.Int(nullable: false),
                        FileSubCat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FileCat_ID, t.FileSubCat_ID });
            
            CreateTable(
                "dbo.SchedTypeSchedule",
                c => new
                    {
                        SchedType_ID = c.Int(nullable: false),
                        Schedule_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SchedType_ID, t.Schedule_ID });
            
            DropForeignKey("dbo.FileSubCat", "FlCat_ID", "dbo.FileCat");
            DropForeignKey("dbo.Schedule", "SchedType_ID", "dbo.SchedType");
            DropIndex("dbo.FileSubCat", new[] { "FlCat_ID" });
            DropIndex("dbo.Schedule", new[] { "SchedType_ID" });
            DropColumn("dbo.FileSubCat", "FlCat_ID");
            DropColumn("dbo.Schedule", "SchedType_ID");
            CreateIndex("dbo.FileCatFileSubCat", "FileSubCat_ID");
            CreateIndex("dbo.FileCatFileSubCat", "FileCat_ID");
            CreateIndex("dbo.SchedTypeSchedule", "Schedule_ID");
            CreateIndex("dbo.SchedTypeSchedule", "SchedType_ID");
            AddForeignKey("dbo.FileCatFileSubCat", "FileSubCat_ID", "dbo.FileSubCat", "ID", cascadeDelete: true);
            AddForeignKey("dbo.FileCatFileSubCat", "FileCat_ID", "dbo.FileCat", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SchedTypeSchedule", "Schedule_ID", "dbo.Schedule", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SchedTypeSchedule", "SchedType_ID", "dbo.SchedType", "ID", cascadeDelete: true);
        }
    }
}
