namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FileCatRoleList", newName: "RoleListFileCat");
            RenameTable(name: "dbo.FileStorageRoleList", newName: "RoleListFileStorage");
            RenameTable(name: "dbo.FileStorageFileCat", newName: "FileCatFileStorage");
            DropForeignKey("dbo.Announcement", "FileStorage_ID", "dbo.FileStorage");
            DropIndex("dbo.Announcement", new[] { "FileStorage_ID" });
            DropPrimaryKey("dbo.RoleListFileCat");
            DropPrimaryKey("dbo.RoleListFileStorage");
            DropPrimaryKey("dbo.FileCatFileStorage");
            CreateTable(
                "dbo.FileStorageAnnouncement",
                c => new
                    {
                        FileStorage_ID = c.Int(nullable: false),
                        Announcement_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FileStorage_ID, t.Announcement_ID })
                .ForeignKey("dbo.FileStorage", t => t.FileStorage_ID, cascadeDelete: true)
                .ForeignKey("dbo.Announcement", t => t.Announcement_ID, cascadeDelete: true)
                .Index(t => t.FileStorage_ID)
                .Index(t => t.Announcement_ID);
            
            AddColumn("dbo.FileStorage", "FileDescription", c => c.String(maxLength: 100));
            AddPrimaryKey("dbo.RoleListFileCat", new[] { "RoleList_ID", "FileCat_ID" });
            AddPrimaryKey("dbo.RoleListFileStorage", new[] { "RoleList_ID", "FileStorage_ID" });
            AddPrimaryKey("dbo.FileCatFileStorage", new[] { "FileCat_ID", "FileStorage_ID" });
            DropColumn("dbo.Announcement", "FileStorage_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Announcement", "FileStorage_ID", c => c.Int());
            DropForeignKey("dbo.FileStorageAnnouncement", "Announcement_ID", "dbo.Announcement");
            DropForeignKey("dbo.FileStorageAnnouncement", "FileStorage_ID", "dbo.FileStorage");
            DropIndex("dbo.FileStorageAnnouncement", new[] { "Announcement_ID" });
            DropIndex("dbo.FileStorageAnnouncement", new[] { "FileStorage_ID" });
            DropPrimaryKey("dbo.FileCatFileStorage");
            DropPrimaryKey("dbo.RoleListFileStorage");
            DropPrimaryKey("dbo.RoleListFileCat");
            DropColumn("dbo.FileStorage", "FileDescription");
            DropTable("dbo.FileStorageAnnouncement");
            AddPrimaryKey("dbo.FileCatFileStorage", new[] { "FileStorage_ID", "FileCat_ID" });
            AddPrimaryKey("dbo.RoleListFileStorage", new[] { "FileStorage_ID", "RoleList_ID" });
            AddPrimaryKey("dbo.RoleListFileCat", new[] { "FileCat_ID", "RoleList_ID" });
            CreateIndex("dbo.Announcement", "FileStorage_ID");
            AddForeignKey("dbo.Announcement", "FileStorage_ID", "dbo.FileStorage", "ID");
            RenameTable(name: "dbo.FileCatFileStorage", newName: "FileStorageFileCat");
            RenameTable(name: "dbo.RoleListFileStorage", newName: "FileStorageRoleList");
            RenameTable(name: "dbo.RoleListFileCat", newName: "FileCatRoleList");
        }
    }
}
