namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditableWelcomeNotice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WelcomeNotice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WelocomeNotice = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WelcomeNotice");
        }
    }
}
