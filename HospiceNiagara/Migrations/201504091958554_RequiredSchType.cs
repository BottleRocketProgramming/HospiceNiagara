namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredSchType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SchedType", "SchedTypeName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SchedType", "SchedTypeName", c => c.String());
        }
    }
}
