namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JoinBoardContWithAppUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BoardContact", "BoardContHomeAddy");
            DropColumn("dbo.BoardContact", "BoardContHomePhone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BoardContact", "BoardContHomePhone", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.BoardContact", "BoardContHomeAddy", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
