namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withPerms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoleList", "IsPerm", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoleList", "IsPerm");
        }
    }
}
