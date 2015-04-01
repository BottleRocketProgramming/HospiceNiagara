namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileSubCat", "FileCatFK_ID", "dbo.FileCat");
            DropIndex("dbo.FileSubCat", new[] { "FileCatFK_ID" });
            CreateTable(
                "dbo.SchedType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchedTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SchedTypeSchedule",
                c => new
                    {
                        SchedType_ID = c.Int(nullable: false),
                        Schedule_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SchedType_ID, t.Schedule_ID })
                .ForeignKey("dbo.SchedType", t => t.SchedType_ID, cascadeDelete: true)
                .ForeignKey("dbo.Schedule", t => t.Schedule_ID, cascadeDelete: true)
                .Index(t => t.SchedType_ID)
                .Index(t => t.Schedule_ID);
            
            CreateTable(
                "dbo.FileCatFileSubCat",
                c => new
                    {
                        FileCat_ID = c.Int(nullable: false),
                        FileSubCat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FileCat_ID, t.FileSubCat_ID })
                .ForeignKey("dbo.FileCat", t => t.FileCat_ID, cascadeDelete: true)
                .ForeignKey("dbo.FileSubCat", t => t.FileSubCat_ID, cascadeDelete: true)
                .Index(t => t.FileCat_ID)
                .Index(t => t.FileSubCat_ID);
            
            DropColumn("dbo.FileSubCat", "FileCatFK_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileSubCat", "FileCatFK_ID", c => c.Int());
            DropForeignKey("dbo.FileCatFileSubCat", "FileSubCat_ID", "dbo.FileSubCat");
            DropForeignKey("dbo.FileCatFileSubCat", "FileCat_ID", "dbo.FileCat");
            DropForeignKey("dbo.SchedTypeSchedule", "Schedule_ID", "dbo.Schedule");
            DropForeignKey("dbo.SchedTypeSchedule", "SchedType_ID", "dbo.SchedType");
            DropIndex("dbo.FileCatFileSubCat", new[] { "FileSubCat_ID" });
            DropIndex("dbo.FileCatFileSubCat", new[] { "FileCat_ID" });
            DropIndex("dbo.SchedTypeSchedule", new[] { "Schedule_ID" });
            DropIndex("dbo.SchedTypeSchedule", new[] { "SchedType_ID" });
            DropTable("dbo.FileCatFileSubCat");
            DropTable("dbo.SchedTypeSchedule");
            DropTable("dbo.SchedType");
            CreateIndex("dbo.FileSubCat", "FileCatFK_ID");
            AddForeignKey("dbo.FileSubCat", "FileCatFK_ID", "dbo.FileCat", "ID");
        }
    }
}
