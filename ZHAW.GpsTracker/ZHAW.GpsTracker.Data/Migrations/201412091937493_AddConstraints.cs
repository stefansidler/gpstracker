namespace ZHAW.GpsTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConstraints : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Latitude = c.String(),
                        Longitude = c.String(),
                        Speed = c.Double(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Session_Key = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sessions", t => t.Session_Key, cascadeDelete: true)
                .Index(t => t.Session_Key);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Positions", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Session_Key", "dbo.Sessions");
            DropIndex("dbo.Users", new[] { "Session_Key" });
            DropIndex("dbo.Positions", new[] { "User_Id" });
            DropTable("dbo.Sessions");
            DropTable("dbo.Users");
            DropTable("dbo.Positions");
        }
    }
}
