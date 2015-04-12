namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requireDOBonReg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "UserDOB", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "UserDOB", c => c.DateTime());
        }
    }
}
