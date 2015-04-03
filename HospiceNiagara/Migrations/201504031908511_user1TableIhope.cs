namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user1TableIhope : DbMigration
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
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.IdentityUser", t => t.ApplicationUser_Id)
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
                "dbo.IdentityUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        UserFName = c.String(),
                        UserMName = c.String(),
                        UserLName = c.String(),
                        UserDOB = c.DateTime(),
                        UserAddress = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        BoardContact_ID = c.Int(),
                        Announcement_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BoardContact", t => t.BoardContact_ID)
                .ForeignKey("dbo.Announcement", t => t.Announcement_ID)
                .Index(t => t.BoardContact_ID)
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
                        SchedType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SchedType", t => t.SchedType_ID)
                .Index(t => t.SchedType_ID);
            
            CreateTable(
                "dbo.SchedType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchedTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FileSubCat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileSubCatName = c.String(nullable: false),
                        FileCatFK = c.Int(nullable: false),
                        FlCat_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileCat", t => t.FlCat_ID)
                .Index(t => t.FlCat_ID);
            
            CreateTable(
                "dbo.FileCat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileCatName = c.String(),
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
                .ForeignKey("dbo.IdentityUser", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.JobDescription_ID)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUserRoleList",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        RoleList_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.RoleList_ID })
                .ForeignKey("dbo.IdentityUser", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.RoleList", t => t.RoleList_ID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.RoleList_ID);
            
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
                "dbo.FileSubCatFileStorage",
                c => new
                    {
                        FileSubCat_ID = c.Int(nullable: false),
                        FileStorage_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FileSubCat_ID, t.FileStorage_ID })
                .ForeignKey("dbo.FileSubCat", t => t.FileSubCat_ID, cascadeDelete: true)
                .ForeignKey("dbo.FileStorage", t => t.FileStorage_ID, cascadeDelete: true)
                .Index(t => t.FileSubCat_ID)
                .Index(t => t.FileStorage_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.IdentityUser", "Announcement_ID", "dbo.Announcement");
            DropForeignKey("dbo.FileSubCat", "FlCat_ID", "dbo.FileCat");
            DropForeignKey("dbo.FileSubCatFileStorage", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.FileSubCatFileStorage", "FileSubCat_ID", "dbo.FileSubCat");
            DropForeignKey("dbo.ScheduleRoleList", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.ScheduleRoleList", "Schedule_ID", "dbo.Schedule");
            DropForeignKey("dbo.Schedule", "SchedType_ID", "dbo.SchedType");
            DropForeignKey("dbo.MeetingRoleList", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.MeetingRoleList", "Meeting_ID", "dbo.Meeting");
            DropForeignKey("dbo.MeetingFileStorage", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.MeetingFileStorage", "Meeting_ID", "dbo.Meeting");
            DropForeignKey("dbo.RoleListFileStorage", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.RoleListFileStorage", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.Announcement", "ApplicationUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.ApplicationUserRoleList", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.ApplicationUserRoleList", "ApplicationUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.JobDescriptionApplicationUser", "ApplicationUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.JobDescriptionApplicationUser", "JobDescription_ID", "dbo.JobDescription");
            DropForeignKey("dbo.BoardContactJobDescription", "JobDescription_ID", "dbo.JobDescription");
            DropForeignKey("dbo.BoardContactJobDescription", "BoardContact_ID", "dbo.BoardContact");
            DropForeignKey("dbo.IdentityUser", "BoardContact_ID", "dbo.BoardContact");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.RoleListAnnouncement", "Announcement_ID", "dbo.Announcement");
            DropForeignKey("dbo.RoleListAnnouncement", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.FileStorageAnnouncement", "Announcement_ID", "dbo.Announcement");
            DropForeignKey("dbo.FileStorageAnnouncement", "FileStorage_ID", "dbo.FileStorage");
            DropIndex("dbo.FileSubCatFileStorage", new[] { "FileStorage_ID" });
            DropIndex("dbo.FileSubCatFileStorage", new[] { "FileSubCat_ID" });
            DropIndex("dbo.ScheduleRoleList", new[] { "RoleList_ID" });
            DropIndex("dbo.ScheduleRoleList", new[] { "Schedule_ID" });
            DropIndex("dbo.MeetingRoleList", new[] { "RoleList_ID" });
            DropIndex("dbo.MeetingRoleList", new[] { "Meeting_ID" });
            DropIndex("dbo.MeetingFileStorage", new[] { "FileStorage_ID" });
            DropIndex("dbo.MeetingFileStorage", new[] { "Meeting_ID" });
            DropIndex("dbo.RoleListFileStorage", new[] { "FileStorage_ID" });
            DropIndex("dbo.RoleListFileStorage", new[] { "RoleList_ID" });
            DropIndex("dbo.ApplicationUserRoleList", new[] { "RoleList_ID" });
            DropIndex("dbo.ApplicationUserRoleList", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.JobDescriptionApplicationUser", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.JobDescriptionApplicationUser", new[] { "JobDescription_ID" });
            DropIndex("dbo.BoardContactJobDescription", new[] { "JobDescription_ID" });
            DropIndex("dbo.BoardContactJobDescription", new[] { "BoardContact_ID" });
            DropIndex("dbo.RoleListAnnouncement", new[] { "Announcement_ID" });
            DropIndex("dbo.RoleListAnnouncement", new[] { "RoleList_ID" });
            DropIndex("dbo.FileStorageAnnouncement", new[] { "Announcement_ID" });
            DropIndex("dbo.FileStorageAnnouncement", new[] { "FileStorage_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.FileSubCat", new[] { "FlCat_ID" });
            DropIndex("dbo.Schedule", new[] { "SchedType_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUser", new[] { "Announcement_ID" });
            DropIndex("dbo.IdentityUser", new[] { "BoardContact_ID" });
            DropIndex("dbo.Announcement", new[] { "ApplicationUser_Id" });
            DropTable("dbo.FileSubCatFileStorage");
            DropTable("dbo.ScheduleRoleList");
            DropTable("dbo.MeetingRoleList");
            DropTable("dbo.MeetingFileStorage");
            DropTable("dbo.RoleListFileStorage");
            DropTable("dbo.ApplicationUserRoleList");
            DropTable("dbo.JobDescriptionApplicationUser");
            DropTable("dbo.BoardContactJobDescription");
            DropTable("dbo.RoleListAnnouncement");
            DropTable("dbo.FileStorageAnnouncement");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.DeathNotice");
            DropTable("dbo.FileCat");
            DropTable("dbo.FileSubCat");
            DropTable("dbo.SchedType");
            DropTable("dbo.Schedule");
            DropTable("dbo.Meeting");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.BoardContact");
            DropTable("dbo.JobDescription");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.IdentityUser");
            DropTable("dbo.RoleList");
            DropTable("dbo.FileStorage");
            DropTable("dbo.Announcement");
        }
    }
}
