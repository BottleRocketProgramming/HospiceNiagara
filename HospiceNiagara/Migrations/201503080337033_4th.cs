namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4th : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Announcement", "IsEvent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Announcement", "IsEvent", c => c.Byte(nullable: false));
        }
    }
}
