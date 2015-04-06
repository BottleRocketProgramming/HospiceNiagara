namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RSVP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeetingUserRSVP",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ComingYorN = c.Boolean(nullable: false),
                        RSVPNotes = c.String(maxLength: 250),
                        AppUser_Id = c.String(maxLength: 128),
                        MeetingRSVP_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.Meeting", t => t.MeetingRSVP_ID)
                .Index(t => t.AppUser_Id)
                .Index(t => t.MeetingRSVP_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeetingUserRSVP", "MeetingRSVP_ID", "dbo.Meeting");
            DropForeignKey("dbo.MeetingUserRSVP", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MeetingUserRSVP", new[] { "MeetingRSVP_ID" });
            DropIndex("dbo.MeetingUserRSVP", new[] { "AppUser_Id" });
            DropTable("dbo.MeetingUserRSVP");
        }
    }
}
