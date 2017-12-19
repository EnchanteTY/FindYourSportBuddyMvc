using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;

namespace FindYourSportBuddy.BL.Abstract.Repositories
{
    public interface IAttendanceRepository
    {
        List<Attendance> GetFutureAttendances(string userId);
        List<Event> GetParticipatedEvents(string userId);
        int GetAttendanceCountById(int id);
        bool CheckAttendance(string userId, int id);
        bool CheckIfFullyRegistered(int currentAttendance);
        Attendance GetAttendance(int id, string userId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}
