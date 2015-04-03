namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.IdentityUser", "UserNameIndex");
            CreateTable(
                "dbo.ApplicationUserRoleList",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        RoleList_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.RoleList_ID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.RoleList", t => t.RoleList_ID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.RoleList_ID);
            
            AlterColumn("dbo.IdentityUser", "Email", c => c.String());
            AlterColumn("dbo.IdentityUser", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserRoleList", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.ApplicationUserRoleList", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserRoleList", new[] { "RoleList_ID" });
            DropIndex("dbo.ApplicationUserRoleList", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.IdentityUser", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.IdentityUser", "Email", c => c.String(maxLength: 256));
            DropTable("dbo.ApplicationUserRoleList");
            CreateIndex("dbo.IdentityUser", "UserName", unique: true, name: "UserNameIndex");
        }
    }
}
