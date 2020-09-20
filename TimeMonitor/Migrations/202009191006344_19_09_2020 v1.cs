namespace TimeMonitor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19_09_2020v1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reports", "User_User_Id", "dbo.Users");
            DropIndex("dbo.Reports", new[] { "User_User_Id" });
            RenameColumn(table: "dbo.Reports", name: "User_User_Id", newName: "User_id");
            AddColumn("dbo.Reports", "Summary", c => c.String());
            AddColumn("dbo.Reports", "Hours", c => c.Int(nullable: false));
            AddColumn("dbo.Reports", "Minutes", c => c.Int(nullable: false));
            AddColumn("dbo.Reports", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "FIO", c => c.String());
            AlterColumn("dbo.Reports", "User_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Reports", "User_id");
            AddForeignKey("dbo.Reports", "User_id", "dbo.Users", "User_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "User_id", "dbo.Users");
            DropIndex("dbo.Reports", new[] { "User_id" });
            AlterColumn("dbo.Reports", "User_id", c => c.Int());
            DropColumn("dbo.Users", "FIO");
            DropColumn("dbo.Reports", "Date");
            DropColumn("dbo.Reports", "Minutes");
            DropColumn("dbo.Reports", "Hours");
            DropColumn("dbo.Reports", "Summary");
            RenameColumn(table: "dbo.Reports", name: "User_id", newName: "User_User_Id");
            CreateIndex("dbo.Reports", "User_User_Id");
            AddForeignKey("dbo.Reports", "User_User_Id", "dbo.Users", "User_Id");
        }
    }
}
