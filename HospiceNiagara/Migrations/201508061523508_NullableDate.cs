namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "UserDOB", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "UserDOB", c => c.DateTime(nullable: false));
        }
    }
}
