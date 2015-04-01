namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class redo2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Announcement", "AnnounceStartDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Announcement", "AnnounceStartDate", c => c.DateTime(nullable: false));
        }
    }
}
