namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBack2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileSubCat", "FileCatFK", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileSubCat", "FileCatFK");
        }
    }
}
