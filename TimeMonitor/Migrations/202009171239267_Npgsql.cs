namespace TimeMonitor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Npgsql : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Rep_id = c.Guid(nullable: false, identity: true),
                        User_User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Rep_id)
                .ForeignKey("dbo.Users", t => t.User_User_Id)
                .Index(t => t.User_User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        User_Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "User_User_Id", "dbo.Users");
            DropIndex("dbo.Reports", new[] { "User_User_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Reports");
        }
    }
}
