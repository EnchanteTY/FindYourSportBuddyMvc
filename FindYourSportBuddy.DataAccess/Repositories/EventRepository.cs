using FindYourSportBuddy.BL.Abstract.Repositories;
using FindYourSportBuddy.BL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FindYourSportBuddy.DataAccess.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Event> GetUpcomingEvents(int currentAttendances, string query = null)
        {
            var upcomingEvents = _context.Events
                .Include(e => e.Category)
                .Where(e => e.DateTime > DateTime.Now &&
                     !e.IsCancelled && e.ParticipantsRequired > currentAttendances);


            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingEvents = upcomingEvents
                    .Where(e =>
                            e.OrganizerName.Contains(query) ||
                            e.Category.Name.Contains(query) ||
                            e.Venue.Contains(query) ||
                            e.Name.Contains(query));
            }

            return upcomingEvents.ToList();
        }

        public IEnumerable<Event> GetUpcomingEventsByUserName(string userName)
        {
            return _context.Events
                .Where(e => e.OrganizerName == userName && e.DateTime > DateTime.Now && !e.IsCancelled);
        }

        public Event GetEventByIdAndUserName(int id, string userName)
        {
            return _context.Events.Single(e => e.Id == id && e.OrganizerName == userName);
        }

        public Event GetEventIncludingAttendance(int id, string userName)
        {
            return _context.Events
               .Include(e => e.Attendances.Select(a => a.Attendee))
               .SingleOrDefault(e => e.Id == id && e.OrganizerName == userName);
        }

        public Event GetEventIncludingCategory(int id)
        {
            return _context.Events
               .Include(e => e.Category)
               .SingleOrDefault(e => e.Id == id);
        }

        public void Add(Event sportevent)
        {
            _context.Events.Add(sportevent);
        }

        public List<Event> GetAllEventsCreatedByUser(string userId)
        {
            return _context.Events
                .Where(e => e.OrganizerId == userId).ToList();
        }

        public List<Event> GetEventsParticipatedByUser(string userId)
        {
            return _context.Attendances.Where(a => a.AttendeeId == userId).Select(a => a.Event).ToList();
        }
    }
}
