namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnnounceStartDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcement", "AnnounceStartDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Announcement", "AnnounceStartDate");
        }
    }
}
