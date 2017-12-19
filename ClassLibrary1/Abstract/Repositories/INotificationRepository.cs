using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;

namespace FindYourSportBuddy.BL.Abstract.Repositories
{
    public interface INotificationRepository
    {
        List<Notification> GetUnreadNotificationWithEvent(string userId);
    }
}
