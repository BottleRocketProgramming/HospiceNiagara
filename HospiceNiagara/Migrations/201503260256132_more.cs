namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class more : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IdentityUser", "BoardContact_ID", c => c.Int());
            CreateIndex("dbo.IdentityUser", "BoardContact_ID");
            AddForeignKey("dbo.IdentityUser", "BoardContact_ID", "dbo.BoardContact", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUser", "BoardContact_ID", "dbo.BoardContact");
            DropIndex("dbo.IdentityUser", new[] { "BoardContact_ID" });
            DropColumn("dbo.IdentityUser", "BoardContact_ID");
        }
    }
}
