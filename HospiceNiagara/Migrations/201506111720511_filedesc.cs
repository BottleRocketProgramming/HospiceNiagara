namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filedesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomepageImage", "FileDesc", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HomepageImage", "FileDesc");
        }
    }
}
