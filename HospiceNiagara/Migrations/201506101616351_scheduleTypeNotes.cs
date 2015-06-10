namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scheduleTypeNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchedType", "SchedTypeNote", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SchedType", "SchedTypeNote");
        }
    }
}
