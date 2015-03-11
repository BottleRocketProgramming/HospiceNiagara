namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileCatFileStorage", "FileCat_ID", "dbo.FileCat");
            DropForeignKey("dbo.FileCatFileStorage", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.RoleListFileCat", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.RoleListFileCat", "FileCat_ID", "dbo.FileCat");
            DropIndex("dbo.FileCatFileStorage", new[] { "FileCat_ID" });
            DropIndex("dbo.FileCatFileStorage", new[] { "FileStorage_ID" });
            DropIndex("dbo.RoleListFileCat", new[] { "RoleList_ID" });
            DropIndex("dbo.RoleListFileCat", new[] { "FileCat_ID" });
            CreateTable(
                "dbo.FileSubCat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileSubCatName = c.String(nullable: false),
                        FileCatFK_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileCat", t => t.FileCatFK_ID)
                .Index(t => t.FileCatFK_ID);
            
            AddColumn("dbo.FileStorage", "FileSubCat_ID", c => c.Int());
            AddColumn("dbo.FileCat", "FileStorage_ID", c => c.Int());
            CreateIndex("dbo.FileStorage", "FileSubCat_ID");
            CreateIndex("dbo.FileCat", "FileStorage_ID");
            AddForeignKey("dbo.FileStorage", "FileSubCat_ID", "dbo.FileSubCat", "ID");
            AddForeignKey("dbo.FileCat", "FileStorage_ID", "dbo.FileStorage", "ID");
            DropTable("dbo.FileCatFileStorage");
            DropTable("dbo.RoleListFileCat");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoleListFileCat",
                c => new
                    {
                        RoleList_ID = c.Int(nullable: false),
                        FileCat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleList_ID, t.FileCat_ID });
            
            CreateTable(
                "dbo.FileCatFileStorage",
                c => new
                    {
                        FileCat_ID = c.Int(nullable: false),
                        FileStorage_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FileCat_ID, t.FileStorage_ID });
            
            DropForeignKey("dbo.FileCat", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.FileStorage", "FileSubCat_ID", "dbo.FileSubCat");
            DropForeignKey("dbo.FileSubCat", "FileCatFK_ID", "dbo.FileCat");
            DropIndex("dbo.FileSubCat", new[] { "FileCatFK_ID" });
            DropIndex("dbo.FileCat", new[] { "FileStorage_ID" });
            DropIndex("dbo.FileStorage", new[] { "FileSubCat_ID" });
            DropColumn("dbo.FileCat", "FileStorage_ID");
            DropColumn("dbo.FileStorage", "FileSubCat_ID");
            DropTable("dbo.FileSubCat");
            CreateIndex("dbo.RoleListFileCat", "FileCat_ID");
            CreateIndex("dbo.RoleListFileCat", "RoleList_ID");
            CreateIndex("dbo.FileCatFileStorage", "FileStorage_ID");
            CreateIndex("dbo.FileCatFileStorage", "FileCat_ID");
            AddForeignKey("dbo.RoleListFileCat", "FileCat_ID", "dbo.FileCat", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RoleListFileCat", "RoleList_ID", "dbo.RoleList", "ID", cascadeDelete: true);
            AddForeignKey("dbo.FileCatFileStorage", "FileStorage_ID", "dbo.FileStorage", "ID", cascadeDelete: true);
            AddForeignKey("dbo.FileCatFileStorage", "FileCat_ID", "dbo.FileCat", "ID", cascadeDelete: true);
        }
    }
}
