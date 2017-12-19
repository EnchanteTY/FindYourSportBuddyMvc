using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindYourSportBuddy.BL.Entities
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; private set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; set; }

        protected UserNotification()
        {

        }

        public UserNotification(Notification notification, ApplicationUser user)
        {
            if (notification == null)
            {
                throw new System.ArgumentNullException(nameof(notification));
            }

            if (user == null)
            {
                throw new System.ArgumentNullException(nameof(user));
            }

            Notification = notification;
            User = user;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}
