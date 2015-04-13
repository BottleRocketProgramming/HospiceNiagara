namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactWorkCell : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffContact", "ContWorkCell", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StaffContact", "ContWorkCell");
        }
    }
}
