namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testing : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "IdentityUser");
            DropForeignKey("dbo.AspNetUsers", "Id", "dbo.IdentityUser");
            DropIndex("dbo.IdentityUser", new[] { "Id" });
            AddColumn("dbo.IdentityUser", "Email", c => c.String());
            AddColumn("dbo.IdentityUser", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.IdentityUser", "PasswordHash", c => c.String());
            AddColumn("dbo.IdentityUser", "SecurityStamp", c => c.String());
            AddColumn("dbo.IdentityUser", "PhoneNumber", c => c.String());
            AddColumn("dbo.IdentityUser", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.IdentityUser", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.IdentityUser", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.IdentityUser", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.IdentityUser", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.IdentityUser", "UserName", c => c.String());
            AddColumn("dbo.IdentityUser", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.IdentityUser", "BoardContact_ID", c => c.Int());
            AddColumn("dbo.IdentityUser", "Announcement_ID", c => c.Int());
            DropTable("dbo.IdentityUser");
        }
        
        public override void Down()
        {
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
                        BoardContact_ID = c.Int(),
                        Announcement_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.IdentityUser", "Announcement_ID");
            DropColumn("dbo.IdentityUser", "BoardContact_ID");
            DropColumn("dbo.IdentityUser", "Discriminator");
            DropColumn("dbo.IdentityUser", "UserName");
            DropColumn("dbo.IdentityUser", "AccessFailedCount");
            DropColumn("dbo.IdentityUser", "LockoutEnabled");
            DropColumn("dbo.IdentityUser", "LockoutEndDateUtc");
            DropColumn("dbo.IdentityUser", "TwoFactorEnabled");
            DropColumn("dbo.IdentityUser", "PhoneNumberConfirmed");
            DropColumn("dbo.IdentityUser", "PhoneNumber");
            DropColumn("dbo.IdentityUser", "SecurityStamp");
            DropColumn("dbo.IdentityUser", "PasswordHash");
            DropColumn("dbo.IdentityUser", "EmailConfirmed");
            DropColumn("dbo.IdentityUser", "Email");
            CreateIndex("dbo.IdentityUser", "Id");
            AddForeignKey("dbo.AspNetUsers", "Id", "dbo.IdentityUser", "Id");
            RenameTable(name: "dbo.IdentityUser", newName: "AspNetUsers");
        }
    }
}
