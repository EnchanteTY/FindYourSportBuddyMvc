using FindYourSportBuddy.BL.Entities;

namespace FindYourSportBuddy.UI.ViewModels
{
    public class EventDetailViewModel
    {
        public Event UpcomingEvent { get; set; }

        public bool IsJoining { get; set; }

        public int CurrentAttendances { get; set; }

        public bool IsFriend { get; set; }
    }
}