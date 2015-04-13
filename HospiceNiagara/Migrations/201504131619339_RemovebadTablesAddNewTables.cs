namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovebadTablesAddNewTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BoardContactApplicationUser", "BoardContact_ID", "dbo.BoardContact");
            DropForeignKey("dbo.BoardContactApplicationUser", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobDescriptionBoardContact", "JobDescription_ID", "dbo.JobDescription");
            DropForeignKey("dbo.JobDescriptionBoardContact", "BoardContact_ID", "dbo.BoardContact");
            DropForeignKey("dbo.JobDescriptionApplicationUser", "JobDescription_ID", "dbo.JobDescription");
            DropForeignKey("dbo.JobDescriptionApplicationUser", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserRoleList", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserRoleList", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.ApplicationUserAnnouncement", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserAnnouncement", "Announcement_ID", "dbo.Announcement");
            DropIndex("dbo.BoardContactApplicationUser", new[] { "BoardContact_ID" });
            DropIndex("dbo.BoardContactApplicationUser", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.JobDescriptionBoardContact", new[] { "JobDescription_ID" });
            DropIndex("dbo.JobDescriptionBoardContact", new[] { "BoardContact_ID" });
            DropIndex("dbo.JobDescriptionApplicationUser", new[] { "JobDescription_ID" });
            DropIndex("dbo.JobDescriptionApplicationUser", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserRoleList", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserRoleList", new[] { "RoleList_ID" });
            DropIndex("dbo.ApplicationUserAnnouncement", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserAnnouncement", new[] { "Announcement_ID" });
            CreateTable(
                "dbo.StaffContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ContExten = c.String(),
                        ContUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ContUser_Id)
                .Index(t => t.ContUser_Id);
            
            CreateTable(
                "dbo.JobDescriptionStaffContact",
                c => new
                    {
                        JobDescription_ID = c.Int(nullable: false),
                        StaffContact_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobDescription_ID, t.StaffContact_ID })
                .ForeignKey("dbo.JobDescription", t => t.JobDescription_ID, cascadeDelete: true)
                .ForeignKey("dbo.StaffContact", t => t.StaffContact_ID, cascadeDelete: true)
                .Index(t => t.JobDescription_ID)
                .Index(t => t.StaffContact_ID);
            
            AddColumn("dbo.BoardContact", "AppUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.BoardContact", "AppUser_Id");
            AddForeignKey("dbo.BoardContact", "AppUser_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.BoardContactApplicationUser");
            DropTable("dbo.JobDescriptionBoardContact");
            DropTable("dbo.JobDescriptionApplicationUser");
            DropTable("dbo.ApplicationUserRoleList");
            DropTable("dbo.ApplicationUserAnnouncement");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserAnnouncement",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Announcement_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Announcement_ID });
            
            CreateTable(
                "dbo.ApplicationUserRoleList",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        RoleList_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.RoleList_ID });
            
            CreateTable(
                "dbo.JobDescriptionApplicationUser",
                c => new
                    {
                        JobDescription_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.JobDescription_ID, t.ApplicationUser_Id });
            
            CreateTable(
                "dbo.JobDescriptionBoardContact",
                c => new
                    {
                        JobDescription_ID = c.Int(nullable: false),
                        BoardContact_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobDescription_ID, t.BoardContact_ID });
            
            CreateTable(
                "dbo.BoardContactApplicationUser",
                c => new
                    {
                        BoardContact_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BoardContact_ID, t.ApplicationUser_Id });
            
            DropForeignKey("dbo.JobDescriptionStaffContact", "StaffContact_ID", "dbo.StaffContact");
            DropForeignKey("dbo.JobDescriptionStaffContact", "JobDescription_ID", "dbo.JobDescription");
            DropForeignKey("dbo.StaffContact", "ContUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BoardContact", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.JobDescriptionStaffContact", new[] { "StaffContact_ID" });
            DropIndex("dbo.JobDescriptionStaffContact", new[] { "JobDescription_ID" });
            DropIndex("dbo.StaffContact", new[] { "ContUser_Id" });
            DropIndex("dbo.BoardContact", new[] { "AppUser_Id" });
            DropColumn("dbo.BoardContact", "AppUser_Id");
            DropTable("dbo.JobDescriptionStaffContact");
            DropTable("dbo.StaffContact");
            CreateIndex("dbo.ApplicationUserAnnouncement", "Announcement_ID");
            CreateIndex("dbo.ApplicationUserAnnouncement", "ApplicationUser_Id");
            CreateIndex("dbo.ApplicationUserRoleList", "RoleList_ID");
            CreateIndex("dbo.ApplicationUserRoleList", "ApplicationUser_Id");
            CreateIndex("dbo.JobDescriptionApplicationUser", "ApplicationUser_Id");
            CreateIndex("dbo.JobDescriptionApplicationUser", "JobDescription_ID");
            CreateIndex("dbo.JobDescriptionBoardContact", "BoardContact_ID");
            CreateIndex("dbo.JobDescriptionBoardContact", "JobDescription_ID");
            CreateIndex("dbo.BoardContactApplicationUser", "ApplicationUser_Id");
            CreateIndex("dbo.BoardContactApplicationUser", "BoardContact_ID");
            AddForeignKey("dbo.ApplicationUserAnnouncement", "Announcement_ID", "dbo.Announcement", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserAnnouncement", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserRoleList", "RoleList_ID", "dbo.RoleList", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserRoleList", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JobDescriptionApplicationUser", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JobDescriptionApplicationUser", "JobDescription_ID", "dbo.JobDescription", "ID", cascadeDelete: true);
            AddForeignKey("dbo.JobDescriptionBoardContact", "BoardContact_ID", "dbo.BoardContact", "ID", cascadeDelete: true);
            AddForeignKey("dbo.JobDescriptionBoardContact", "JobDescription_ID", "dbo.JobDescription", "ID", cascadeDelete: true);
            AddForeignKey("dbo.BoardContactApplicationUser", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BoardContactApplicationUser", "BoardContact_ID", "dbo.BoardContact", "ID", cascadeDelete: true);
        }
    }
}
