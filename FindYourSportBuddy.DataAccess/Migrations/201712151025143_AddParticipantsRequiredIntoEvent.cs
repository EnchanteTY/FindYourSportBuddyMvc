namespace FindYourSportBuddy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParticipantsRequiredIntoEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "ParticipantsRequired", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "ParticipantsRequired");
        }
    }
}
