namespace TimeMonitor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19_09_2020v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Name_1", c => c.String());
            AddColumn("dbo.Users", "Name_2", c => c.String());
            AddColumn("dbo.Users", "Name_3", c => c.String());
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "FIO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "FIO", c => c.String());
            DropColumn("dbo.Users", "IsDeleted");
            DropColumn("dbo.Users", "Name_3");
            DropColumn("dbo.Users", "Name_2");
            DropColumn("dbo.Users", "Name_1");
        }
    }
}
