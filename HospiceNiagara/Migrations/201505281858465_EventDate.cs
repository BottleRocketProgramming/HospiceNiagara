namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meeting", "EventDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Meeting", "EventStart");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meeting", "EventStart", c => c.DateTime(nullable: false));
            DropColumn("dbo.Meeting", "EventDate");
        }
    }
}
