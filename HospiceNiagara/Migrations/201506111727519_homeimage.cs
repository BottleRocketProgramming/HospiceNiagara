namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class homeimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileStorage", "HomeImage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileStorage", "HomeImage");
        }
    }
}
