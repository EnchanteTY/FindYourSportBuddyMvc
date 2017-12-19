using FindYourSportBuddy.BL.Entities;
using System;

namespace FindYourSportBuddy.DataAccess.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UserDTO Organizer { get; set; }

        public DateTime DateTime { get; set; }

        public Region Region { get; set; }

        public string Venue { get; set; }

        public CategoryDTO Category { get; set; }

        public int PeopleRequired { get; set; }

        public bool IsCancelled { get; set; }
    }
}
