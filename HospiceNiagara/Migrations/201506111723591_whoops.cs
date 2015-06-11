namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whoops : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.HomepageImage");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HomepageImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MimeType = c.String(nullable: false, maxLength: 256),
                        FileName = c.String(nullable: false, maxLength: 100),
                        FileDesc = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
