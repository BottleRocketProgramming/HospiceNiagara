namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noFileContent : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FileStorage", "FileContent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileStorage", "FileContent", c => c.Binary(nullable: false));
        }
    }
}
