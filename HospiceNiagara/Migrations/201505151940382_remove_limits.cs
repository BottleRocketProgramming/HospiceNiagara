namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_limits : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Announcement", "AnnounceText", c => c.String(nullable: false));
            AlterColumn("dbo.Meeting", "EventDiscription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Meeting", "EventDiscription", c => c.String(nullable: false, maxLength: 2000));
            AlterColumn("dbo.Announcement", "AnnounceText", c => c.String(nullable: false, maxLength: 2000));
        }
    }
}
