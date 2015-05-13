namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventsDesc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Announcement", "AnnounceText", c => c.String(nullable: false, maxLength: 2000));
            AlterColumn("dbo.Meeting", "EventDiscription", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Meeting", "EventDiscription", c => c.String(nullable: false, maxLength: 510));
            AlterColumn("dbo.Announcement", "AnnounceText", c => c.String(nullable: false, maxLength: 1020));
        }
    }
}
