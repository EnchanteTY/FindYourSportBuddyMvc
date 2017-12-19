using FindYourSportBuddy.BL.Abstract.Repositories;
using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FindYourSportBuddy.DataAccess.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Notification> GetUnreadNotificationWithEvent(string userId)
        {
            return _context.UserNotifications
                .Where(u => u.UserId == userId && u.IsRead == false)
                .Select(u => u.Notification)
                .Include(n => n.Event)
                .ToList();
        }
    }
}
