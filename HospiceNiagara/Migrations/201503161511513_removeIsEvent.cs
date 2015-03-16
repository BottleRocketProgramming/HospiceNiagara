namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeIsEvent : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Announcement", "AnnounceStartDate");
            DropColumn("dbo.Announcement", "IsEvent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Announcement", "IsEvent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Announcement", "AnnounceStartDate", c => c.DateTime());
        }
    }
}
