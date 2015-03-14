namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CatsAndSubCats : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileStorage", "FileSubCat_ID", "dbo.FileSubCat");
            DropForeignKey("dbo.FileCat", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.FileStorage", "FileSortType_ID", "dbo.FileSortType");
            DropIndex("dbo.FileStorage", new[] { "FileSubCat_ID" });
            DropIndex("dbo.FileStorage", new[] { "FileSortType_ID" });
            DropIndex("dbo.FileCat", new[] { "FileStorage_ID" });
            CreateTable(
                "dbo.FileSubCatFileStorage",
                c => new
                    {
                        FileSubCat_ID = c.Int(nullable: false),
                        FileStorage_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FileSubCat_ID, t.FileStorage_ID })
                .ForeignKey("dbo.FileSubCat", t => t.FileSubCat_ID, cascadeDelete: true)
                .ForeignKey("dbo.FileStorage", t => t.FileStorage_ID, cascadeDelete: true)
                .Index(t => t.FileSubCat_ID)
                .Index(t => t.FileStorage_ID);
            
            DropColumn("dbo.FileStorage", "FileSubCat_ID");
            DropColumn("dbo.FileStorage", "FileSortType_ID");
            DropColumn("dbo.FileCat", "FileStorage_ID");
            DropTable("dbo.FileSortType");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FileSortType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileSrtDefintion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.FileCat", "FileStorage_ID", c => c.Int());
            AddColumn("dbo.FileStorage", "FileSortType_ID", c => c.Int());
            AddColumn("dbo.FileStorage", "FileSubCat_ID", c => c.Int());
            DropForeignKey("dbo.FileSubCatFileStorage", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.FileSubCatFileStorage", "FileSubCat_ID", "dbo.FileSubCat");
            DropIndex("dbo.FileSubCatFileStorage", new[] { "FileStorage_ID" });
            DropIndex("dbo.FileSubCatFileStorage", new[] { "FileSubCat_ID" });
            DropTable("dbo.FileSubCatFileStorage");
            CreateIndex("dbo.FileCat", "FileStorage_ID");
            CreateIndex("dbo.FileStorage", "FileSortType_ID");
            CreateIndex("dbo.FileStorage", "FileSubCat_ID");
            AddForeignKey("dbo.FileStorage", "FileSortType_ID", "dbo.FileSortType", "ID");
            AddForeignKey("dbo.FileCat", "FileStorage_ID", "dbo.FileStorage", "ID");
            AddForeignKey("dbo.FileStorage", "FileSubCat_ID", "dbo.FileSubCat", "ID");
        }
    }
}
