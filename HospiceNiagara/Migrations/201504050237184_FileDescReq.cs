namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileDescReq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FileStorage", "FileDescription", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FileStorage", "FileDescription", c => c.String(maxLength: 100));
        }
    }
}
