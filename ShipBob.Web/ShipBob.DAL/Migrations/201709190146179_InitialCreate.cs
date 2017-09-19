namespace ShipBob.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicationUserID = c.Int(nullable: false),
                        TrackingNumber = c.String(),
                        Name = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUserID, cascadeDelete: true)
                .Index(t => t.ApplicationUserID);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "ApplicationUserID", "dbo.ApplicationUser");
            DropIndex("dbo.Order", new[] { "ApplicationUserID" });
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Order");
        }
    }
}
