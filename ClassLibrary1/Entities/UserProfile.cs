namespace FindYourSportBuddy.BL.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public byte Age { get; set; }

        public Gender Gender { get; set; }

        public Region PreferSportRegion { get; set; }

        public string PreferSportVenue { get; set; }

        public string PreferSportTime { get; set; }

        public byte InterestedSportId { get; set; }


        public virtual Category InterestedSport { get; set; }

        public string Introduction { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
