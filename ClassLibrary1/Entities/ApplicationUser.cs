using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FindYourSportBuddy.BL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public ICollection<UserNotification> UserNotifications { get; set; }
        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<FriendRequest> FriendRequests { get; set; }
        public ICollection<FriendRequest> Requesters { get; set; }
        public ICollection<FriendRequest> Friends { get; set; }

        public ApplicationUser()
        {
            UserNotifications = new Collection<UserNotification>();
            Followers = new Collection<Following>();
            Followees = new Collection<Following>();
            FriendRequests = new Collection<FriendRequest>();
            Requesters = new Collection<FriendRequest>();
            Friends = new Collection<FriendRequest>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public void Notify(Notification notification)
        {
            var userNotification = new UserNotification(notification, this);
            UserNotifications.Add(userNotification);
        }
    }
}
