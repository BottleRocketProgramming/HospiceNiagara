namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Announcement", "AnnounceText", c => c.String());
            AlterColumn("dbo.Meeting", "EventDiscription", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Meeting", "EventDiscription", c => c.String(nullable: false));
            AlterColumn("dbo.Announcement", "AnnounceText", c => c.String(nullable: false));
        }
    }
}
