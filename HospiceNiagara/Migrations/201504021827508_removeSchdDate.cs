namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeSchdDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Schedule", "SchedStartDate");
            DropColumn("dbo.Schedule", "SchedEndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedule", "SchedEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Schedule", "SchedStartDate", c => c.DateTime(nullable: false));
        }
    }
}
