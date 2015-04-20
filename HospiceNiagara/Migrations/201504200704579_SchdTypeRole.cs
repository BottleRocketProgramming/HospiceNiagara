namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchdTypeRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchedTypeRoleList",
                c => new
                    {
                        SchedType_ID = c.Int(nullable: false),
                        RoleList_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SchedType_ID, t.RoleList_ID })
                .ForeignKey("dbo.SchedType", t => t.SchedType_ID, cascadeDelete: true)
                .ForeignKey("dbo.RoleList", t => t.RoleList_ID, cascadeDelete: true)
                .Index(t => t.SchedType_ID)
                .Index(t => t.RoleList_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchedTypeRoleList", "RoleList_ID", "dbo.RoleList");
            DropForeignKey("dbo.SchedTypeRoleList", "SchedType_ID", "dbo.SchedType");
            DropIndex("dbo.SchedTypeRoleList", new[] { "RoleList_ID" });
            DropIndex("dbo.SchedTypeRoleList", new[] { "SchedType_ID" });
            DropTable("dbo.SchedTypeRoleList");
        }
    }
}
