namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventWithTimes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meeting", "EventTime", c => c.String(nullable: false));
            DropColumn("dbo.Meeting", "EventEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meeting", "EventEnd", c => c.DateTime(nullable: false));
            DropColumn("dbo.Meeting", "EventTime");
        }
    }
}
