namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RSVPnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MeetingUserRSVP", "ComingYorN", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MeetingUserRSVP", "ComingYorN", c => c.Boolean(nullable: false));
        }
    }
}
