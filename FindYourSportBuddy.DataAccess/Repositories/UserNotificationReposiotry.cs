using FindYourSportBuddy.BL.Abstract.Repositories;
using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FindYourSportBuddy.DataAccess.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private AppDbContext _context;

        public UserNotificationRepository(AppDbContext context)
        {
            _context = context;
        }


        public List<UserNotification> GetUnreadUserNotification(string userId)
        {
            return _context.UserNotifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToList();
        }
    }
}
