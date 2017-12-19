using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;

namespace FindYourSportBuddy.UI.ViewModels
{
    public class UserEventViewModel
    {
        public List<Event> EventsCreated { get; set; }
        public List<Event> EventsParticpated { get; set; }
    }
}