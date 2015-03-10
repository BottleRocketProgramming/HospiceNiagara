namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uploadDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileStorage", "FileUploadDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileStorage", "FileUploadDate");
        }
    }
}
