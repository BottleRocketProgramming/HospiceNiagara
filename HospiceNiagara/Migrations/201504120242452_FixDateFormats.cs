namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDateFormats : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoleList", "RoleName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoleList", "RoleName", c => c.String());
        }
    }
}
