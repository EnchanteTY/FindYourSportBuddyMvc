using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;

namespace FindYourSportBuddy.BL.Abstract.Repositories
{
    public interface IFriendRequestRepository
    {

        FriendRequest GetFriendRequestSent(string userId, string friendId);
        FriendRequest GetPendingRequest(string userId, string friendId);
        List<ApplicationUser> GetAllFriends(string userId);
        bool CheckIfTwoAreFriends(string userId, string friendId);

    }
}
