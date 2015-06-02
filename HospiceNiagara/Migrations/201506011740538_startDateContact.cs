namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class startDateContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contact", "StartDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contact", "StartDate");
        }
    }
}
