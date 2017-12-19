using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FindYourSportBuddy.UI.ViewModels
{
    public class EventViewModel
    {
        public IEnumerable<Event> Events { get; set; }

        public string Heading { get; set; }

        public bool ShowJoinButton { get; set; }

        public int currentAttendanceCount { get; set; }

        public string SearchTerm { get; set; }

        public ILookup<int, Attendance> Attendances { get; set; }

        public ILookup<string, ApplicationUser> Friends { get; set; }
    }
}