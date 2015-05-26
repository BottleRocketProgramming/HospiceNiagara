namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noContacts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BoardContact", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StaffContact", "ContUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobDescriptionStaffContact", "JobDescription_ID", "dbo.JobDescription");
            DropForeignKey("dbo.JobDescriptionStaffContact", "StaffContact_ID", "dbo.StaffContact");
            DropIndex("dbo.BoardContact", new[] { "AppUser_Id" });
            DropIndex("dbo.StaffContact", new[] { "ContUser_Id" });
            DropIndex("dbo.JobDescriptionStaffContact", new[] { "JobDescription_ID" });
            DropIndex("dbo.JobDescriptionStaffContact", new[] { "StaffContact_ID" });
            DropTable("dbo.BoardContact");
            DropTable("dbo.StaffContact");
            DropTable("dbo.JobDescriptionStaffContact");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.JobDescriptionStaffContact",
                c => new
                    {
                        JobDescription_ID = c.Int(nullable: false),
                        StaffContact_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobDescription_ID, t.StaffContact_ID });
            
            CreateTable(
                "dbo.StaffContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ContExten = c.String(),
                        ContWorkCell = c.String(),
                        ContUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BoardContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoardContPosition = c.String(nullable: false, maxLength: 100),
                        BoardContWorkAddy = c.String(maxLength: 250),
                        BoardContWorkPhone = c.String(maxLength: 25),
                        BoardContCellPhone = c.String(maxLength: 25),
                        BoardContFaxNum = c.String(maxLength: 25),
                        BoardContPartnerName = c.String(maxLength: 100),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.JobDescriptionStaffContact", "StaffContact_ID");
            CreateIndex("dbo.JobDescriptionStaffContact", "JobDescription_ID");
            CreateIndex("dbo.StaffContact", "ContUser_Id");
            CreateIndex("dbo.BoardContact", "AppUser_Id");
            AddForeignKey("dbo.JobDescriptionStaffContact", "StaffContact_ID", "dbo.StaffContact", "ID", cascadeDelete: true);
            AddForeignKey("dbo.JobDescriptionStaffContact", "JobDescription_ID", "dbo.JobDescription", "ID", cascadeDelete: true);
            AddForeignKey("dbo.StaffContact", "ContUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BoardContact", "AppUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
