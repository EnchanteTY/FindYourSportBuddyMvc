namespace FindYourSportBuddy.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateCategory : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories (Id,Name) VALUES (1,'Running')");
            Sql("INSERT INTO Categories (Id,Name) VALUES (2,'Cycling')");
            Sql("INSERT INTO Categories (Id,Name) VALUES (3,'Basketball')");
            Sql("INSERT INTO Categories (Id,Name) VALUES (4,'Football')");
            Sql("INSERT INTO Categories (Id,Name) VALUES (5,'Badminton')");
            Sql("INSERT INTO Categories (Id,Name) VALUES (6,'Gym')");
        }

        public override void Down()
        {
            Sql("DELETE FROM Categories WHERE Id IN(1,2,3,4,5,6)");
        }
    }
}
