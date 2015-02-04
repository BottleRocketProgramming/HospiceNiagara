namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AnnounceText = c.String(nullable: false, maxLength: 1020),
                        AnnounceEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BoardContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoardContPosition = c.String(nullable: false, maxLength: 100),
                        BoardContHomeAddy = c.String(nullable: false, maxLength: 250),
                        BoardContWorkAddy = c.String(maxLength: 250),
                        BoardContHomePhone = c.String(nullable: false, maxLength: 25),
                        BoardContWorkPhone = c.String(maxLength: 25),
                        BoardContCellPhone = c.String(maxLength: 25),
                        BoardContFaxNum = c.String(maxLength: 25),
                        BoardContPartnerName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DeathNotice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DnFirstName = c.String(nullable: false, maxLength: 50),
                        DnMiddleName = c.String(maxLength: 50),
                        DnLastName = c.String(nullable: false, maxLength: 50),
                        DnLocation = c.String(maxLength: 150),
                        DnNotes = c.String(maxLength: 510),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FileSortType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileSrtDefintion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FileStorage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileContent = c.Binary(nullable: false),
                        MimeType = c.String(nullable: false, maxLength: 256),
                        FileName = c.String(nullable: false, maxLength: 100),
                        FileSortTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileSortType", t => t.FileSortTypeID)
                .Index(t => t.FileSortTypeID);
            
            CreateTable(
                "dbo.JobDescription",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false, maxLength: 100),
                        JobDescpt = c.String(nullable: false, maxLength: 510),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MeetingOrEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventTitle = c.String(nullable: false, maxLength: 100),
                        EventDiscription = c.String(nullable: false, maxLength: 510),
                        EventLocation = c.String(nullable: false, maxLength: 510),
                        EventStart = c.DateTime(nullable: false),
                        EventEnd = c.DateTime(nullable: false),
                        EventRequirments = c.String(maxLength: 510),
                        EventLinks = c.String(maxLength: 510),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserFName = c.String(),
                        UserMName = c.String(),
                        UserLName = c.String(),
                        UserDOB = c.DateTime(),
                        UserAddress = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.FileStorage", "FileSortTypeID", "dbo.FileSortType");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.FileStorage", new[] { "FileSortTypeID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MeetingOrEvent");
            DropTable("dbo.JobDescription");
            DropTable("dbo.FileStorage");
            DropTable("dbo.FileSortType");
            DropTable("dbo.DeathNotice");
            DropTable("dbo.BoardContact");
            DropTable("dbo.Announcement");
        }
    }
}
