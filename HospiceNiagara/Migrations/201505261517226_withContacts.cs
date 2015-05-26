namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withContacts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Position = c.String(),
                        Extention = c.String(),
                        CellNumber = c.String(),
                        ContactType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContactType", t => t.ContactType_ID)
                .Index(t => t.ContactType_ID);
            
            CreateTable(
                "dbo.ContactType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ContactTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "ContactType_ID", "dbo.ContactType");
            DropIndex("dbo.Contact", new[] { "ContactType_ID" });
            DropTable("dbo.ContactType");
            DropTable("dbo.Contact");
        }
    }
}
