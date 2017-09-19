namespace ShipBob.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredapplicationuserfields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUser", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.ApplicationUser", "FirstName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUser", "FirstName", c => c.String());
            AlterColumn("dbo.ApplicationUser", "LastName", c => c.String());
        }
    }
}
