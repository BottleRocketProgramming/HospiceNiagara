namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1st : DbMigration
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
                        IsEvent = c.Boolean(nullable: false),
                        FileStorage_ID = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileStorage", t => t.FileStorage_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.FileStorage_ID)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FileCat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileCatName = c.String(),
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
                "dbo.FileSortType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileSrtDefintion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Meeting",
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
                "dbo.Schedule",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchedName = c.String(nullable: false, maxLength: 50),
                        SchedLink = c.String(nullable: false, maxLength: 510),
                        SchedStartDate = c.DateTime(nullable: false),
                        SchedEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        Announcement_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Announcement", t => t.Announcement_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Announcement_ID);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.IdentityUser", t => t.IdentityUser_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
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
                "dbo.JobDescription",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false, maxLength: 100),
                        JobDescpt = c.String(nullable: false, maxLength: 510),
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.RoleAnnouncement",
                c => new
                    {
                        Role_ID = c.Int(nullable: false),
                        Announcement_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_ID, t.Announcement_ID })
                .ForeignKey("dbo.Role", t => t.Role_ID, cascadeDelete: true)
                .ForeignKey("dbo.Announcement", t => t.Announcement_ID, cascadeDelete: true)
                .Index(t => t.Role_ID)
                .Index(t => t.Announcement_ID);
            
            CreateTable(
                "dbo.FileStorageFileCat",
                c => new
                    {
                        FileStorage_ID = c.Int(nullable: false),
                        FileCat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FileStorage_ID, t.FileCat_ID })
                .ForeignKey("dbo.FileStorage", t => t.FileStorage_ID, cascadeDelete: true)
                .ForeignKey("dbo.FileCat", t => t.FileCat_ID, cascadeDelete: true)
                .Index(t => t.FileStorage_ID)
                .Index(t => t.FileCat_ID);
            
            CreateTable(
                "dbo.FileStorageRole",
                c => new
                    {
                        FileStorage_ID = c.Int(nullable: false),
                        Role_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FileStorage_ID, t.Role_ID })
                .ForeignKey("dbo.FileStorage", t => t.FileStorage_ID, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.FileStorage_ID)
                .Index(t => t.Role_ID);
            
            CreateTable(
                "dbo.MeetingFileStorage",
                c => new
                    {
                        Meeting_ID = c.Int(nullable: false),
                        FileStorage_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meeting_ID, t.FileStorage_ID })
                .ForeignKey("dbo.Meeting", t => t.Meeting_ID, cascadeDelete: true)
                .ForeignKey("dbo.FileStorage", t => t.FileStorage_ID, cascadeDelete: true)
                .Index(t => t.Meeting_ID)
                .Index(t => t.FileStorage_ID);
            
            CreateTable(
                "dbo.MeetingRole",
                c => new
                    {
                        Meeting_ID = c.Int(nullable: false),
                        Role_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meeting_ID, t.Role_ID })
                .ForeignKey("dbo.Meeting", t => t.Meeting_ID, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.Meeting_ID)
                .Index(t => t.Role_ID);
            
            CreateTable(
                "dbo.FileCatRole",
                c => new
                    {
                        FileCat_ID = c.Int(nullable: false),
                        Role_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FileCat_ID, t.Role_ID })
                .ForeignKey("dbo.FileCat", t => t.FileCat_ID, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.FileCat_ID)
                .Index(t => t.Role_ID);
            
            CreateTable(
                "dbo.ScheduleRole",
                c => new
                    {
                        Schedule_ID = c.Int(nullable: false),
                        Role_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Schedule_ID, t.Role_ID })
                .ForeignKey("dbo.Schedule", t => t.Schedule_ID, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.Schedule_ID)
                .Index(t => t.Role_ID);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.IdentityUser", "Announcement_ID", "dbo.Announcement");
            DropForeignKey("dbo.UserJobDesc", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserJobDesc", "JobDescriptions_ID", "dbo.JobDescription");
            DropForeignKey("dbo.UserJobDesc", "IdentityUsers_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.Announcement", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.ScheduleRole", "Role_ID", "dbo.Role");
            DropForeignKey("dbo.ScheduleRole", "Schedule_ID", "dbo.Schedule");
            DropForeignKey("dbo.FileCatRole", "Role_ID", "dbo.Role");
            DropForeignKey("dbo.FileCatRole", "FileCat_ID", "dbo.FileCat");
            DropForeignKey("dbo.MeetingRole", "Role_ID", "dbo.Role");
            DropForeignKey("dbo.MeetingRole", "Meeting_ID", "dbo.Meeting");
            DropForeignKey("dbo.MeetingFileStorage", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.MeetingFileStorage", "Meeting_ID", "dbo.Meeting");
            DropForeignKey("dbo.FileStorageRole", "Role_ID", "dbo.Role");
            DropForeignKey("dbo.FileStorageRole", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.FileStorage", "FileSortTypeID", "dbo.FileSortType");
            DropForeignKey("dbo.FileStorageFileCat", "FileCat_ID", "dbo.FileCat");
            DropForeignKey("dbo.FileStorageFileCat", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.Announcement", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.RoleAnnouncement", "Announcement_ID", "dbo.Announcement");
            DropForeignKey("dbo.RoleAnnouncement", "Role_ID", "dbo.Role");
            DropIndex("dbo.AspNetUsers", new[] { "Id" });
            DropIndex("dbo.ScheduleRole", new[] { "Role_ID" });
            DropIndex("dbo.ScheduleRole", new[] { "Schedule_ID" });
            DropIndex("dbo.FileCatRole", new[] { "Role_ID" });
            DropIndex("dbo.FileCatRole", new[] { "FileCat_ID" });
            DropIndex("dbo.MeetingRole", new[] { "Role_ID" });
            DropIndex("dbo.MeetingRole", new[] { "Meeting_ID" });
            DropIndex("dbo.MeetingFileStorage", new[] { "FileStorage_ID" });
            DropIndex("dbo.MeetingFileStorage", new[] { "Meeting_ID" });
            DropIndex("dbo.FileStorageRole", new[] { "Role_ID" });
            DropIndex("dbo.FileStorageRole", new[] { "FileStorage_ID" });
            DropIndex("dbo.FileStorageFileCat", new[] { "FileCat_ID" });
            DropIndex("dbo.FileStorageFileCat", new[] { "FileStorage_ID" });
            DropIndex("dbo.RoleAnnouncement", new[] { "Announcement_ID" });
            DropIndex("dbo.RoleAnnouncement", new[] { "Role_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserJobDesc", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserJobDesc", new[] { "JobDescriptions_ID" });
            DropIndex("dbo.UserJobDesc", new[] { "IdentityUsers_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUser", new[] { "Announcement_ID" });
            DropIndex("dbo.IdentityUser", "UserNameIndex");
            DropIndex("dbo.FileStorage", new[] { "FileSortTypeID" });
            DropIndex("dbo.Announcement", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Announcement", new[] { "FileStorage_ID" });
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ScheduleRole");
            DropTable("dbo.FileCatRole");
            DropTable("dbo.MeetingRole");
            DropTable("dbo.MeetingFileStorage");
            DropTable("dbo.FileStorageRole");
            DropTable("dbo.FileStorageFileCat");
            DropTable("dbo.RoleAnnouncement");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.DeathNotice");
            DropTable("dbo.BoardContact");
            DropTable("dbo.JobDescription");
            DropTable("dbo.UserJobDesc");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.IdentityUser");
            DropTable("dbo.Schedule");
            DropTable("dbo.Meeting");
            DropTable("dbo.FileSortType");
            DropTable("dbo.FileStorage");
            DropTable("dbo.FileCat");
            DropTable("dbo.Role");
            DropTable("dbo.Announcement");
        }
    }
}
