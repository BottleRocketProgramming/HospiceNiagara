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
                        ApplicationUser_Id = c.String(maxLength: 128),
                        FileStorageVM_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.FileStorageVM", t => t.FileStorageVM_ID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.FileStorageVM_ID);
            
            CreateTable(
                "dbo.FileStorage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileContent = c.Binary(nullable: false),
                        MimeType = c.String(nullable: false, maxLength: 256),
                        FileName = c.String(nullable: false, maxLength: 100),
                        FileDescription = c.String(maxLength: 100),
                        FileSortTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileSortType", t => t.FileSortTypeID)
                .Index(t => t.FileSortTypeID);
            
            CreateTable(
                "dbo.FileCat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileCatName = c.String(),
                        FileStorageVM_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileStorageVM", t => t.FileStorageVM_ID)
                .Index(t => t.FileStorageVM_ID);
            
            CreateTable(
                "dbo.RoleList",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        FileStorageVM_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileStorageVM", t => t.FileStorageVM_ID)
                .Index(t => t.FileStorageVM_ID);
            
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
                        FileStorageVM_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileStorageVM", t => t.FileStorageVM_ID)
                .Index(t => t.FileStorageVM_ID);
            
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
                "dbo.FileSortType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileSrtDefintion = c.String(nullable: false),
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
                "dbo.FileStorageVM",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MimeType = c.String(nullable: false, maxLength: 256),
                        FileName = c.String(nullable: false, maxLength: 100),
                        FileDescription = c.String(maxLength: 100),
                        FileSortTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileSortType", t => t.FileSortTypeID)
                .Index(t => t.FileSortTypeID);
            
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
                "dbo.FileStorageAnnouncement",
                c => new
                    {
                        FileStorage_ID = c.Int(nullable: false),
                        Announcement_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FileStorage_ID, t.Announcement_ID })
                .ForeignKey("dbo.FileStorage", t => t.FileStorage_ID, cascadeDelete: true)
                .ForeignKey("dbo.Announcement", t => t.Announcement_ID, cascadeDelete: true)
                .Index(t => t.FileStorage_ID)
                .Index(t => t.Announcement_ID);
            
            CreateTable(
                "dbo.FileCatFileStorage",
                c => new
                    {
                        FileCat_ID = c.Int(nullable: false),
                        FileStorage_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FileCat_ID, t.FileStorage_ID })
                .ForeignKey("dbo.FileCat", t => t.FileCat_ID, cascadeDelete: true)
                .ForeignKey("dbo.FileStorage", t => t.FileStorage_ID, cascadeDelete: true)
                .Index(t => t.FileCat_ID)
                .Index(t => t.FileStorage_ID);
            
            CreateTable(
                "dbo.RoleListAnnouncement",
                c => new
                    {
                        RoleList_ID = c.Int(nullable: false),
                        Announcement_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleList_ID, t.Announcement_ID })
                .ForeignKey("dbo.RoleList", t => t.RoleList_ID, cascadeDelete: true)
                .ForeignKey("dbo.Announcement", t => t.Announcement_ID, cascadeDelete: true)
                .Index(t => t.RoleList_ID)
                .Index(t => t.Announcement_ID);
            
            CreateTable(
                "dbo.RoleListFileCat",
                c => new
                    {
                        RoleList_ID = c.Int(nullable: false),
                        FileCat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleList_ID, t.FileCat_ID })
                .ForeignKey("dbo.RoleList", t => t.RoleList_ID, cascadeDelete: true)
                .ForeignKey("dbo.FileCat", t => t.FileCat_ID, cascadeDelete: true)
                .Index(t => t.RoleList_ID)
                .Index(t => t.FileCat_ID);
            
            CreateTable(
                "dbo.RoleListFileStorage",
                c => new
                    {
                        RoleList_ID = c.Int(nullable: false),
                        FileStorage_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleList_ID, t.FileStorage_ID })
                .ForeignKey("dbo.RoleList", t => t.RoleList_ID, cascadeDelete: true)
                .ForeignKey("dbo.FileStorage", t => t.FileStorage_ID, cascadeDelete: true)
                .Index(t => t.RoleList_ID)
                .Index(t => t.FileStorage_ID);
            
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
                "dbo.MeetingRoleList",
                c => new
                    {
                        Meeting_ID = c.Int(nullable: false),
                        RoleList_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meeting_ID, t.RoleList_ID })
                .ForeignKey("dbo.Meeting", t => t.Meeting_ID, cascadeDelete: true)
                .ForeignKey("dbo.RoleList", t => t.RoleList_ID, cascadeDelete: true)
                .Index(t => t.Meeting_ID)
                .Index(t => t.RoleList_ID);
            
            CreateTable(
                "dbo.ScheduleRoleList",
                c => new
                    {
                        Schedule_ID = c.Int(nullable: false),
                        RoleList_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Schedule_ID, t.RoleList_ID })
                .ForeignKey("dbo.Schedule", t => t.Schedule_ID, cascadeDelete: true)
                .ForeignKey("dbo.RoleList", t => t.RoleList_ID, cascadeDelete: true)
                .Index(t => t.Schedule_ID)
                .Index(t => t.RoleList_ID);
            
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
            DropForeignKey("dbo.Meeting", "FileStorageVM_ID", "dbo.FileStorageVM");
            DropForeignKey("dbo.RoleList", "FileStorageVM_ID", "dbo.FileStorageVM");
            DropForeignKey("dbo.FileStorageVM", "FileSortTypeID", "dbo.FileSortType");
            DropForeignKey("dbo.FileCat", "FileStorageVM_ID", "dbo.FileStorageVM");
            DropForeignKey("dbo.Announcement", "FileStorageVM_ID", "dbo.FileStorageVM");
            DropForeignKey("dbo.IdentityUser", "Announcement_ID", "dbo.Announcement");
            DropForeignKey("dbo.UserJobDesc", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserJobDesc", "JobDescriptions_ID", "dbo.JobDescription");
            DropForeignKey("dbo.UserJobDesc", "IdentityUsers_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.Announcement", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.FileStorage", "FileSortTypeID", "dbo.FileSortType");
            DropForeignKey("dbo.ScheduleRoleList", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.ScheduleRoleList", "Schedule_ID", "dbo.Schedule");
            DropForeignKey("dbo.MeetingRoleList", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.MeetingRoleList", "Meeting_ID", "dbo.Meeting");
            DropForeignKey("dbo.MeetingFileStorage", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.MeetingFileStorage", "Meeting_ID", "dbo.Meeting");
            DropForeignKey("dbo.RoleListFileStorage", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.RoleListFileStorage", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.RoleListFileCat", "FileCat_ID", "dbo.FileCat");
            DropForeignKey("dbo.RoleListFileCat", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.RoleListAnnouncement", "Announcement_ID", "dbo.Announcement");
            DropForeignKey("dbo.RoleListAnnouncement", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.FileCatFileStorage", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.FileCatFileStorage", "FileCat_ID", "dbo.FileCat");
            DropForeignKey("dbo.FileStorageAnnouncement", "Announcement_ID", "dbo.Announcement");
            DropForeignKey("dbo.FileStorageAnnouncement", "FileStorage_ID", "dbo.FileStorage");
            DropIndex("dbo.AspNetUsers", new[] { "Id" });
            DropIndex("dbo.ScheduleRoleList", new[] { "RoleList_ID" });
            DropIndex("dbo.ScheduleRoleList", new[] { "Schedule_ID" });
            DropIndex("dbo.MeetingRoleList", new[] { "RoleList_ID" });
            DropIndex("dbo.MeetingRoleList", new[] { "Meeting_ID" });
            DropIndex("dbo.MeetingFileStorage", new[] { "FileStorage_ID" });
            DropIndex("dbo.MeetingFileStorage", new[] { "Meeting_ID" });
            DropIndex("dbo.RoleListFileStorage", new[] { "FileStorage_ID" });
            DropIndex("dbo.RoleListFileStorage", new[] { "RoleList_ID" });
            DropIndex("dbo.RoleListFileCat", new[] { "FileCat_ID" });
            DropIndex("dbo.RoleListFileCat", new[] { "RoleList_ID" });
            DropIndex("dbo.RoleListAnnouncement", new[] { "Announcement_ID" });
            DropIndex("dbo.RoleListAnnouncement", new[] { "RoleList_ID" });
            DropIndex("dbo.FileCatFileStorage", new[] { "FileStorage_ID" });
            DropIndex("dbo.FileCatFileStorage", new[] { "FileCat_ID" });
            DropIndex("dbo.FileStorageAnnouncement", new[] { "Announcement_ID" });
            DropIndex("dbo.FileStorageAnnouncement", new[] { "FileStorage_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.FileStorageVM", new[] { "FileSortTypeID" });
            DropIndex("dbo.UserJobDesc", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserJobDesc", new[] { "JobDescriptions_ID" });
            DropIndex("dbo.UserJobDesc", new[] { "IdentityUsers_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUser", new[] { "Announcement_ID" });
            DropIndex("dbo.IdentityUser", "UserNameIndex");
            DropIndex("dbo.Meeting", new[] { "FileStorageVM_ID" });
            DropIndex("dbo.RoleList", new[] { "FileStorageVM_ID" });
            DropIndex("dbo.FileCat", new[] { "FileStorageVM_ID" });
            DropIndex("dbo.FileStorage", new[] { "FileSortTypeID" });
            DropIndex("dbo.Announcement", new[] { "FileStorageVM_ID" });
            DropIndex("dbo.Announcement", new[] { "ApplicationUser_Id" });
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ScheduleRoleList");
            DropTable("dbo.MeetingRoleList");
            DropTable("dbo.MeetingFileStorage");
            DropTable("dbo.RoleListFileStorage");
            DropTable("dbo.RoleListFileCat");
            DropTable("dbo.RoleListAnnouncement");
            DropTable("dbo.FileCatFileStorage");
            DropTable("dbo.FileStorageAnnouncement");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.FileStorageVM");
            DropTable("dbo.DeathNotice");
            DropTable("dbo.BoardContact");
            DropTable("dbo.JobDescription");
            DropTable("dbo.UserJobDesc");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.IdentityUser");
            DropTable("dbo.FileSortType");
            DropTable("dbo.Schedule");
            DropTable("dbo.Meeting");
            DropTable("dbo.RoleList");
            DropTable("dbo.FileCat");
            DropTable("dbo.FileStorage");
            DropTable("dbo.Announcement");
        }
    }
}
