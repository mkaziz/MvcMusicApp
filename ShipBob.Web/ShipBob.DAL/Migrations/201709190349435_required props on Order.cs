namespace ShipBob.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredpropsonOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "TrackingNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Order", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Order", "StreetAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Order", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Order", "State", c => c.String(nullable: false));
            AlterColumn("dbo.Order", "Zip", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "Zip", c => c.String());
            AlterColumn("dbo.Order", "State", c => c.String());
            AlterColumn("dbo.Order", "City", c => c.String());
            AlterColumn("dbo.Order", "StreetAddress", c => c.String());
            AlterColumn("dbo.Order", "Name", c => c.String());
            AlterColumn("dbo.Order", "TrackingNumber", c => c.String());
        }
    }
}
