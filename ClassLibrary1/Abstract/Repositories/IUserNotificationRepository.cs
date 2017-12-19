using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;

namespace FindYourSportBuddy.BL.Abstract.Repositories
{
    public interface IUserNotificationRepository
    {

        List<UserNotification> GetUnreadUserNotification(string userId);
    }
}
