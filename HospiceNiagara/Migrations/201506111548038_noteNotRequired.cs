namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noteNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SchedType", "SchedTypeNote", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SchedType", "SchedTypeNote", c => c.String(nullable: false));
        }
    }
}
