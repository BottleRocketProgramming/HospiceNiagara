namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Announcement_Title : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcement", "AnnounceTitle", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Announcement", "AnnounceTitle");
        }
    }
}
