namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class auditing_fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcement", "UploadDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Announcement", "UploadedBy", c => c.String());
            AddColumn("dbo.FileStorage", "UploadedBy", c => c.String());
            AddColumn("dbo.Meeting", "UploadDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Meeting", "UploadedBy", c => c.String());
            DropColumn("dbo.Meeting", "EventRequirments");
            DropColumn("dbo.Meeting", "EventLinks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meeting", "EventLinks", c => c.String(maxLength: 510));
            AddColumn("dbo.Meeting", "EventRequirments", c => c.String(maxLength: 510));
            DropColumn("dbo.Meeting", "UploadedBy");
            DropColumn("dbo.Meeting", "UploadDate");
            DropColumn("dbo.FileStorage", "UploadedBy");
            DropColumn("dbo.Announcement", "UploadedBy");
            DropColumn("dbo.Announcement", "UploadDate");
        }
    }
}
