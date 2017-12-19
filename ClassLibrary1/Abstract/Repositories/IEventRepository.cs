using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;

namespace FindYourSportBuddy.BL.Abstract.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetUpcomingEvents(int currentAttendances, string query);
        IEnumerable<Event> GetUpcomingEventsByUserName(string userName);
        Event GetEventByIdAndUserName(int id, string userName);
        Event GetEventIncludingAttendance(int id, string userName);
        Event GetEventIncludingCategory(int id);
        void Add(Event sportevent);
        List<Event> GetAllEventsCreatedByUser(string userId);
        List<Event> GetEventsParticipatedByUser(string userId);
    }
}
