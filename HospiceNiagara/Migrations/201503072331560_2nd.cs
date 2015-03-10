namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2nd : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FileStorage", new[] { "FileSortTypeID" });
            RenameColumn(table: "dbo.FileStorage", name: "FileSortTypeID", newName: "FileSortType_ID");
            AlterColumn("dbo.FileStorage", "FileSortType_ID", c => c.Int());
            CreateIndex("dbo.FileStorage", "FileSortType_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FileStorage", new[] { "FileSortType_ID" });
            AlterColumn("dbo.FileStorage", "FileSortType_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.FileStorage", name: "FileSortType_ID", newName: "FileSortTypeID");
            CreateIndex("dbo.FileStorage", "FileSortTypeID");
        }
    }
}
