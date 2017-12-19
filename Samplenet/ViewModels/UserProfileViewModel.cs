using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;

namespace FindYourSportBuddy.UI.ViewModels
{
    public class UserProfileViewModel
    {
        public UserProfile UserProfile { get; set; }

        public string Category { get; set; }

        public List<UserProfile> UserProfiles { get; set; }

        public bool IsFollowing { get; set; }

        public bool PendingFriendRequest { get; set; }

        public bool IsFriend { get; set; }

        public bool FriendRequestSent { get; set; }
    }
}