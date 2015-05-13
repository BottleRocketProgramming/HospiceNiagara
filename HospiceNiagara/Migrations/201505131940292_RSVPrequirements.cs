namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RSVPrequirements : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeetingUserRSVP", "AdminRequirements", c => c.String(maxLength: 250));
            AddColumn("dbo.MeetingUserRSVP", "UserRequirements", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MeetingUserRSVP", "UserRequirements");
            DropColumn("dbo.MeetingUserRSVP", "AdminRequirements");
        }
    }
}
