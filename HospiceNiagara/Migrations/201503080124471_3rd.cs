namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3rd : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FileStorageVM", new[] { "FileSortTypeID" });
            RenameColumn(table: "dbo.FileStorageVM", name: "FileSortTypeID", newName: "FileSortType_ID");
            AlterColumn("dbo.FileStorageVM", "FileSortType_ID", c => c.Int());
            CreateIndex("dbo.FileStorageVM", "FileSortType_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FileStorageVM", new[] { "FileSortType_ID" });
            AlterColumn("dbo.FileStorageVM", "FileSortType_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.FileStorageVM", name: "FileSortType_ID", newName: "FileSortTypeID");
            CreateIndex("dbo.FileStorageVM", "FileSortTypeID");
        }
    }
}
