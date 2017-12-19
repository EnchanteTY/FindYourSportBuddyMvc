using FindYourSportBuddy.BL.Entities;
using System;

namespace FindYourSportBuddy.DataAccess.DTOs
{
    public class NotificationDTO
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public EventDTO Event { get; set; }
    }
}
