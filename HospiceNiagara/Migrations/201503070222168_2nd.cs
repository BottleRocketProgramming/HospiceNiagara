namespace HospiceNiagara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2nd : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Role", newName: "RoleList");
            RenameTable(name: "dbo.RoleAnnouncement", newName: "RoleListAnnouncement");
            RenameTable(name: "dbo.FileCatRole", newName: "FileCatRoleList");
            RenameTable(name: "dbo.FileStorageRole", newName: "FileStorageRoleList");
            RenameTable(name: "dbo.MeetingRole", newName: "MeetingRoleList");
            RenameTable(name: "dbo.ScheduleRole", newName: "ScheduleRoleList");
            RenameColumn(table: "dbo.RoleListAnnouncement", name: "Role_ID", newName: "RoleList_ID");
            RenameColumn(table: "dbo.FileCatRoleList", name: "Role_ID", newName: "RoleList_ID");
            RenameColumn(table: "dbo.FileStorageRoleList", name: "Role_ID", newName: "RoleList_ID");
            RenameColumn(table: "dbo.MeetingRoleList", name: "Role_ID", newName: "RoleList_ID");
            RenameColumn(table: "dbo.ScheduleRoleList", name: "Role_ID", newName: "RoleList_ID");
            RenameIndex(table: "dbo.RoleListAnnouncement", name: "IX_Role_ID", newName: "IX_RoleList_ID");
            RenameIndex(table: "dbo.FileStorageRoleList", name: "IX_Role_ID", newName: "IX_RoleList_ID");
            RenameIndex(table: "dbo.MeetingRoleList", name: "IX_Role_ID", newName: "IX_RoleList_ID");
            RenameIndex(table: "dbo.FileCatRoleList", name: "IX_Role_ID", newName: "IX_RoleList_ID");
            RenameIndex(table: "dbo.ScheduleRoleList", name: "IX_Role_ID", newName: "IX_RoleList_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ScheduleRoleList", name: "IX_RoleList_ID", newName: "IX_Role_ID");
            RenameIndex(table: "dbo.FileCatRoleList", name: "IX_RoleList_ID", newName: "IX_Role_ID");
            RenameIndex(table: "dbo.MeetingRoleList", name: "IX_RoleList_ID", newName: "IX_Role_ID");
            RenameIndex(table: "dbo.FileStorageRoleList", name: "IX_RoleList_ID", newName: "IX_Role_ID");
            RenameIndex(table: "dbo.RoleListAnnouncement", name: "IX_RoleList_ID", newName: "IX_Role_ID");
            RenameColumn(table: "dbo.ScheduleRoleList", name: "RoleList_ID", newName: "Role_ID");
            RenameColumn(table: "dbo.MeetingRoleList", name: "RoleList_ID", newName: "Role_ID");
            RenameColumn(table: "dbo.FileStorageRoleList", name: "RoleList_ID", newName: "Role_ID");
            RenameColumn(table: "dbo.FileCatRoleList", name: "RoleList_ID", newName: "Role_ID");
            RenameColumn(table: "dbo.RoleListAnnouncement", name: "RoleList_ID", newName: "Role_ID");
            RenameTable(name: "dbo.ScheduleRoleList", newName: "ScheduleRole");
            RenameTable(name: "dbo.MeetingRoleList", newName: "MeetingRole");
            RenameTable(name: "dbo.FileStorageRoleList", newName: "FileStorageRole");
            RenameTable(name: "dbo.FileCatRoleList", newName: "FileCatRole");
            RenameTable(name: "dbo.RoleListAnnouncement", newName: "RoleAnnouncement");
            RenameTable(name: "dbo.RoleList", newName: "Role");
        }
    }
}
