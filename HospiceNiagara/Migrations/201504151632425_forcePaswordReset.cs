namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forcePaswordReset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PasswordChanged", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PasswordChanged");
        }
    }
}
