namespace TimeMonitor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20_09_2020v1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "IsDeleted");
        }
    }
}
