namespace FindYourSportBuddy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsPrivateIntoEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "IsPrivate", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "IsPrivate");
        }
    }
}
