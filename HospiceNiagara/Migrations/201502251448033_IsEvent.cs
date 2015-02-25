namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcement", "IsEvent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Announcement", "IsEvent");
        }
    }
}
