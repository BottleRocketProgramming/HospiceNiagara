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
                "dbo.AnnouncementUserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WasViewed = c.Boolean(nullable: false),
                        Announcements_ID = c.Int(),
                        IdentityRoles_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Announcement", t => t.Announcements_ID)
                .ForeignKey("dbo.AspNetRoles", t => t.IdentityRoles_Id)
                .Index(t => t.Announcements_ID)
                .Index(t => t.IdentityRoles_Id);
            
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
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.IdentityUser", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UserAnnouncement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WasViewed = c.Boolean(nullable: false),
                        Announcements_ID = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityUsers_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Announcement", t => t.Announcements_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityUser", t => t.IdentityUsers_Id)
                .Index(t => t.Announcements_ID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityUsers_Id);
            
            CreateTable(
                "dbo.IdentityUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUser", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.IdentityUser", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UserEventRSVP",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HasRSVPed = c.Boolean(nullable: false),
                        IdentityUsers_Id = c.String(maxLength: 128),
                        JobDescriptions_ID = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.IdentityUser", t => t.IdentityUsers_Id)
                .ForeignKey("dbo.JobDescription", t => t.JobDescriptions_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityUsers_Id)
                .Index(t => t.JobDescriptions_ID)
                .Index(t => t.ApplicationUser_Id);
            
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
                "dbo.UserJobDesc",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdentityUsers_Id = c.String(maxLength: 128),
                        JobDescriptions_ID = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.IdentityUser", t => t.IdentityUsers_Id)
                .ForeignKey("dbo.JobDescription", t => t.JobDescriptions_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityUsers_Id)
                .Index(t => t.JobDescriptions_ID)
                .Index(t => t.ApplicationUser_Id);
            
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
                "dbo.FileStoreUserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FielStorages_ID = c.Int(),
                        IdentityUsers_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileStorage", t => t.FielStorages_ID)
                .ForeignKey("dbo.IdentityUser", t => t.IdentityUsers_Id)
                .Index(t => t.FielStorages_ID)
                .Index(t => t.IdentityUsers_Id);
            
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
                "dbo.MeetingOrEventUserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdentityRoles_Id = c.String(maxLength: 128),
                        MettingOrEvents_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetRoles", t => t.IdentityRoles_Id)
                .ForeignKey("dbo.MeetingOrEvent", t => t.MettingOrEvents_ID)
                .Index(t => t.IdentityRoles_Id)
                .Index(t => t.MettingOrEvents_ID);
            
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUser", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Id", "dbo.IdentityUser");
            DropForeignKey("dbo.MeetingOrEventUserRole", "MettingOrEvents_ID", "dbo.MeetingOrEvent");
            DropForeignKey("dbo.MeetingOrEventUserRole", "IdentityRoles_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.FileStoreUserRole", "IdentityUsers_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.FileStoreUserRole", "FielStorages_ID", "dbo.FileStorage");
            DropForeignKey("dbo.FileStorage", "FileSortTypeID", "dbo.FileSortType");
            DropForeignKey("dbo.UserAnnouncement", "IdentityUsers_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.UserJobDesc", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserJobDesc", "JobDescriptions_ID", "dbo.JobDescription");
            DropForeignKey("dbo.UserJobDesc", "IdentityUsers_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.UserEventRSVP", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserEventRSVP", "JobDescriptions_ID", "dbo.JobDescription");
            DropForeignKey("dbo.UserEventRSVP", "IdentityUsers_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.UserAnnouncement", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.UserAnnouncement", "Announcements_ID", "dbo.Announcement");
            DropForeignKey("dbo.AnnouncementUserRole", "IdentityRoles_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AnnouncementUserRole", "Announcements_ID", "dbo.Announcement");
            DropIndex("dbo.AspNetUsers", new[] { "Id" });
            DropIndex("dbo.MeetingOrEventUserRole", new[] { "MettingOrEvents_ID" });
            DropIndex("dbo.MeetingOrEventUserRole", new[] { "IdentityRoles_Id" });
            DropIndex("dbo.FileStoreUserRole", new[] { "IdentityUsers_Id" });
            DropIndex("dbo.FileStoreUserRole", new[] { "FielStorages_ID" });
            DropIndex("dbo.FileStorage", new[] { "FileSortTypeID" });
            DropIndex("dbo.UserJobDesc", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserJobDesc", new[] { "JobDescriptions_ID" });
            DropIndex("dbo.UserJobDesc", new[] { "IdentityUsers_Id" });
            DropIndex("dbo.UserEventRSVP", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserEventRSVP", new[] { "JobDescriptions_ID" });
            DropIndex("dbo.UserEventRSVP", new[] { "IdentityUsers_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUser", "UserNameIndex");
            DropIndex("dbo.UserAnnouncement", new[] { "IdentityUsers_Id" });
            DropIndex("dbo.UserAnnouncement", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserAnnouncement", new[] { "Announcements_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AnnouncementUserRole", new[] { "IdentityRoles_Id" });
            DropIndex("dbo.AnnouncementUserRole", new[] { "Announcements_ID" });
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.MeetingOrEventUserRole");
            DropTable("dbo.MeetingOrEvent");
            DropTable("dbo.FileStoreUserRole");
            DropTable("dbo.FileStorage");
            DropTable("dbo.FileSortType");
            DropTable("dbo.DeathNotice");
            DropTable("dbo.BoardContact");
            DropTable("dbo.UserJobDesc");
            DropTable("dbo.JobDescription");
            DropTable("dbo.UserEventRSVP");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.IdentityUser");
            DropTable("dbo.UserAnnouncement");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AnnouncementUserRole");
            DropTable("dbo.Announcement");
        }
    }
}
