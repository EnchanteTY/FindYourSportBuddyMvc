namespace FindYourSportBuddy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowingAndFriend : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Events", name: "Organizer_Id", newName: "OrganizerId");
            RenameIndex(table: "dbo.Events", name: "IX_Organizer_Id", newName: "IX_OrganizerId");
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        FollowerId = c.String(nullable: false, maxLength: 128),
                        FolloweeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FolloweeId })
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId)
                .ForeignKey("dbo.AspNetUsers", t => t.FolloweeId)
                .Index(t => t.FollowerId)
                .Index(t => t.FolloweeId);
            
            CreateTable(
                "dbo.FriendRequests",
                c => new
                    {
                        RequesterId = c.String(nullable: false, maxLength: 128),
                        FriendId = c.String(nullable: false, maxLength: 128),
                        IsAccepted = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RequesterId, t.FriendId })
                .ForeignKey("dbo.AspNetUsers", t => t.FriendId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.RequesterId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .Index(t => t.RequesterId)
                .Index(t => t.FriendId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendRequests", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "RequesterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "FriendId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "FollowerId", "dbo.AspNetUsers");
            DropIndex("dbo.FriendRequests", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.FriendRequests", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FriendRequests", new[] { "FriendId" });
            DropIndex("dbo.FriendRequests", new[] { "RequesterId" });
            DropIndex("dbo.Followings", new[] { "FolloweeId" });
            DropIndex("dbo.Followings", new[] { "FollowerId" });
            DropTable("dbo.FriendRequests");
            DropTable("dbo.Followings");
            RenameIndex(table: "dbo.Events", name: "IX_OrganizerId", newName: "IX_Organizer_Id");
            RenameColumn(table: "dbo.Events", name: "OrganizerId", newName: "Organizer_Id");
        }
    }
}
