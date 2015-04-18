namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class syncUp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FileCat", "FileCatName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FileCat", "FileCatName", c => c.String());
        }
    }
}
