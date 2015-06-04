namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dunno : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.JobDescription");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.JobDescription",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false, maxLength: 100),
                        JobDescpt = c.String(nullable: false, maxLength: 510),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
