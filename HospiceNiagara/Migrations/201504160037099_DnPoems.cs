namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DnPoems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeathNoticePoems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DnPoem = c.String(nullable: false, maxLength: 1020),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DeathNoticePoems");
        }
    }
}
