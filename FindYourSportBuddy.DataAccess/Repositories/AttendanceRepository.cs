using FindYourSportBuddy.BL.Abstract.Repositories;
using FindYourSportBuddy.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FindYourSportBuddy.DataAccess.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private AppDbContext _context;

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Event.DateTime > DateTime.Now)
                .ToList();

        }

        public List<Event> GetParticipatedEvents(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Event)
                .ToList();
        }

        public int GetAttendanceCountById(int id)
        {
            return _context.Attendances
                  .Where(a => a.EventId == id).Select(a => a.Attendee).Count();
        }

        public bool CheckAttendance(string userId, int id)
        {
            return _context.Attendances.Any(a => a.AttendeeId == userId && a.EventId == id);
        }

        public bool CheckIfFullyRegistered(int currentAttendance)
        {
            if (currentAttendance == 0)
            {
                return false;
            }
            return _context.Attendances.Any(a => a.Event.ParticipantsRequired > currentAttendance);
        }

        public Attendance GetAttendance(int id, string userId)
        {
            return _context.Attendances.SingleOrDefault(a => a.EventId == id && a.AttendeeId == userId);
        }

        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }
    }
}
