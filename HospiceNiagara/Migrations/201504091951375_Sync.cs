namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sync : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Schedule", new[] { "SchedType_ID" });
            AlterColumn("dbo.Schedule", "SchedType_ID", c => c.Int());
            CreateIndex("dbo.Schedule", "SchedType_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Schedule", new[] { "SchedType_ID" });
            AlterColumn("dbo.Schedule", "SchedType_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Schedule", "SchedType_ID");
        }
    }
}
