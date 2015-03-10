namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withBoardContactJobDesc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoardContactJobDescription",
                c => new
                    {
                        BoardContact_ID = c.Int(nullable: false),
                        JobDescription_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BoardContact_ID, t.JobDescription_ID })
                .ForeignKey("dbo.BoardContact", t => t.BoardContact_ID, cascadeDelete: true)
                .ForeignKey("dbo.JobDescription", t => t.JobDescription_ID, cascadeDelete: true)
                .Index(t => t.BoardContact_ID)
                .Index(t => t.JobDescription_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoardContactJobDescription", "JobDescription_ID", "dbo.JobDescription");
            DropForeignKey("dbo.BoardContactJobDescription", "BoardContact_ID", "dbo.BoardContact");
            DropIndex("dbo.BoardContactJobDescription", new[] { "JobDescription_ID" });
            DropIndex("dbo.BoardContactJobDescription", new[] { "BoardContact_ID" });
            DropTable("dbo.BoardContactJobDescription");
        }
    }
}
