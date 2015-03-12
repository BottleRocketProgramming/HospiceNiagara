namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class redo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AnnounceText = c.String(nullable: false, maxLength: 1020),
                        AnnounceStartDate = c.DateTime(nullable: false),
                        AnnounceEndDate = c.DateTime(nullable: false),
                        IsEvent = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.FileStorage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileContent = c.Binary(nullable: false),
                        MimeType = c.String(nullable: false, maxLength: 256),
                        FileName = c.String(nullable: false, maxLength: 100),
                        FileDescription = c.String(maxLength: 100),
                        FileUploadDate = c.DateTime(nullable: false),
                        FileSubCat_ID = c.Int(),
                        FileSortType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileSubCat", t => t.FileSubCat_ID)
                .ForeignKey("dbo.FileSortType", t => t.FileSortType_ID)
                .Index(t => t.FileSubCat_ID)
                .Index(t => t.FileSortType_ID);
            
            CreateTable(
                "dbo.FileCat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileCatName = c.String(),
                        FileStorage_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileStorage", t => t.FileStorage_ID)
                .Index(t => t.FileStorage_ID);
            
            CreateTable(
                "dbo.FileSubCat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileSubCatName = c.String(nullable: false),
                        FileCatFK_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileCat", t => t.FileCatFK_ID)
                .Index(t => t.FileCatFK_ID);
            
            CreateTable(
                "dbo.FileSortType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileSrtDefintion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoleList",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
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
                "dbo.BoardContactJobDescription",
                c => new
                    {
                        BoardContact_ID = c.Int(nullable: false),
                        JobDescription_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BoardContact_ID, t.JobDescription_ID })
                .ForeignKey("dbo.BoardContact", t => t.BoardContact_ID, cascadeDelete: true)
                .ForeignKey("dbo.JobDescription", t => t.JobDescription_ID, cascadeDelete: true)
                .Index(t => t.BoardContact_ID)
                .Index(t => t.JobDescription_ID);
            
            CreateTable(
                "dbo.JobDescriptionApplicationUser",
                c => new
                    {
                        JobDescription_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.JobDescription_ID, t.ApplicationUser_Id })
                .ForeignKey("dbo.JobDescription", t => t.JobDescription_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.JobDescription_ID)
                .Index(t => t.ApplicationUser_Id);
            
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
            DropForeignKey("dbo.Announcement", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobDescriptionApplicationUser", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobDescriptionApplicationUser", "JobDescription_ID", "dbo.JobDescription");
            DropForeignKey("dbo.BoardContactJobDescription", "JobDescription_ID", "dbo.JobDescription");
            DropForeignKey("dbo.BoardContactJobDescription", "BoardContact_ID", "dbo.BoardContact");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.ScheduleRoleList", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.ScheduleRoleList", "Schedule_ID", "dbo.Schedule");
            DropForeignKey("dbo.MeetingRoleList", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.MeetingRoleList", "Meeting_ID", "dbo.Meeting");
            DropForeignKey("dbo.MeetingFileStorage", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.MeetingFileStorage", "Meeting_ID", "dbo.Meeting");
            DropForeignKey("dbo.RoleListFileStorage", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.RoleListFileStorage", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.RoleListAnnouncement", "Announcement_ID", "dbo.Announcement");
            DropForeignKey("dbo.RoleListAnnouncement", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.FileStorage", "FileSortType_ID", "dbo.FileSortType");
            DropForeignKey("dbo.FileCat", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.FileStorage", "FileSubCat_ID", "dbo.FileSubCat");
            DropForeignKey("dbo.FileSubCat", "FileCatFK_ID", "dbo.FileCat");
            DropForeignKey("dbo.FileStorageAnnouncement", "Announcement_ID", "dbo.Announcement");
            DropForeignKey("dbo.FileStorageAnnouncement", "FileStorage_ID", "dbo.FileStorage");
            DropIndex("dbo.AspNetUsers", new[] { "Id" });
            DropIndex("dbo.JobDescriptionApplicationUser", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.JobDescriptionApplicationUser", new[] { "JobDescription_ID" });
            DropIndex("dbo.BoardContactJobDescription", new[] { "JobDescription_ID" });
            DropIndex("dbo.BoardContactJobDescription", new[] { "BoardContact_ID" });
            DropIndex("dbo.ScheduleRoleList", new[] { "RoleList_ID" });
            DropIndex("dbo.ScheduleRoleList", new[] { "Schedule_ID" });
            DropIndex("dbo.MeetingRoleList", new[] { "RoleList_ID" });
            DropIndex("dbo.MeetingRoleList", new[] { "Meeting_ID" });
            DropIndex("dbo.MeetingFileStorage", new[] { "FileStorage_ID" });
            DropIndex("dbo.MeetingFileStorage", new[] { "Meeting_ID" });
            DropIndex("dbo.RoleListFileStorage", new[] { "FileStorage_ID" });
            DropIndex("dbo.RoleListFileStorage", new[] { "RoleList_ID" });
            DropIndex("dbo.RoleListAnnouncement", new[] { "Announcement_ID" });
            DropIndex("dbo.RoleListAnnouncement", new[] { "RoleList_ID" });
            DropIndex("dbo.FileStorageAnnouncement", new[] { "Announcement_ID" });
            DropIndex("dbo.FileStorageAnnouncement", new[] { "FileStorage_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUser", new[] { "Announcement_ID" });
            DropIndex("dbo.IdentityUser", "UserNameIndex");
            DropIndex("dbo.FileSubCat", new[] { "FileCatFK_ID" });
            DropIndex("dbo.FileCat", new[] { "FileStorage_ID" });
            DropIndex("dbo.FileStorage", new[] { "FileSortType_ID" });
            DropIndex("dbo.FileStorage", new[] { "FileSubCat_ID" });
            DropIndex("dbo.Announcement", new[] { "ApplicationUser_Id" });
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.JobDescriptionApplicationUser");
            DropTable("dbo.BoardContactJobDescription");
            DropTable("dbo.ScheduleRoleList");
            DropTable("dbo.MeetingRoleList");
            DropTable("dbo.MeetingFileStorage");
            DropTable("dbo.RoleListFileStorage");
            DropTable("dbo.RoleListAnnouncement");
            DropTable("dbo.FileStorageAnnouncement");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.DeathNotice");
            DropTable("dbo.BoardContact");
            DropTable("dbo.JobDescription");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.IdentityUser");
            DropTable("dbo.Schedule");
            DropTable("dbo.Meeting");
            DropTable("dbo.RoleList");
            DropTable("dbo.FileSortType");
            DropTable("dbo.FileSubCat");
            DropTable("dbo.FileCat");
            DropTable("dbo.FileStorage");
            DropTable("dbo.Announcement");
        }
    }
}
