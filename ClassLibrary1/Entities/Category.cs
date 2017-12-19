namespace FindYourSportBuddy.BL.Entities
{
    public class Category
    {
        public byte Id { get; set; }
        public string Name { get; set; }
    }

    public enum Region
    {
        North, West, Central, East, South
    }

    public enum Gender
    {
        M, F
    }
}
