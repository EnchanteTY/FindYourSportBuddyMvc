namespace FindYourSportBuddy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryAndUserProfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.String(),
                        Age = c.Byte(nullable: false),
                        Gender = c.Int(nullable: false),
                        PreferSportRegion = c.Int(nullable: false),
                        PreferSportVenue = c.String(),
                        PreferSportTime = c.String(),
                        InterestedSportId = c.Byte(nullable: false),
                        Introduction = c.String(),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.InterestedSportId, cascadeDelete: true)
                .Index(t => t.InterestedSportId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "InterestedSportId", "dbo.Categories");
            DropIndex("dbo.UserProfiles", new[] { "InterestedSportId" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Categories");
        }
    }
}
