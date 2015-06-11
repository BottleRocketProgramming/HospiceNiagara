namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedtypefiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchedTypeFileStorage",
                c => new
                    {
                        SchedType_ID = c.Int(nullable: false),
                        FileStorage_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SchedType_ID, t.FileStorage_ID })
                .ForeignKey("dbo.SchedType", t => t.SchedType_ID, cascadeDelete: true)
                .ForeignKey("dbo.FileStorage", t => t.FileStorage_ID, cascadeDelete: true)
                .Index(t => t.SchedType_ID)
                .Index(t => t.FileStorage_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchedTypeFileStorage", "FileStorage_ID", "dbo.FileStorage");
            DropForeignKey("dbo.SchedTypeFileStorage", "SchedType_ID", "dbo.SchedType");
            DropIndex("dbo.SchedTypeFileStorage", new[] { "FileStorage_ID" });
            DropIndex("dbo.SchedTypeFileStorage", new[] { "SchedType_ID" });
            DropTable("dbo.SchedTypeFileStorage");
        }
    }
}
