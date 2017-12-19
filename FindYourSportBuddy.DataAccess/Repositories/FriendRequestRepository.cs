using FindYourSportBuddy.BL.Abstract.Repositories;
using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FindYourSportBuddy.DataAccess.Repositories
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private AppDbContext _context;

        public FriendRequestRepository(AppDbContext context)
        {
            _context = context;
        }



        public FriendRequest GetFriendRequestSent(string userId, string friendId)
        {
            return _context.FriendRequests.SingleOrDefault(f => f.RequesterId == userId && f.FriendId == friendId);
        }

        public FriendRequest GetPendingRequest(string userId, string friendId)
        {
            return _context.FriendRequests.SingleOrDefault(f => f.RequesterId == friendId && f.FriendId == userId);
        }

        public List<ApplicationUser> GetAllFriends(string userId)
        {
            var friends = _context.FriendRequests
              .Where(f => f.FriendId == userId && f.IsAccepted)
              .Select(f => f.Requester);
            var myfriends = _context.FriendRequests
                .Where(f => f.RequesterId == userId && f.IsAccepted)
                .Select(f => f.Friend);
            return friends.Union(myfriends).ToList(); ;
        }

        public bool CheckIfTwoAreFriends(string userId, string friendId)
        {
            return _context.FriendRequests.Any(f => f.RequesterId == userId && f.FriendId == friendId && f.IsAccepted) ||
                _context.FriendRequests.Any(f => f.FriendId == userId && f.RequesterId == friendId && f.IsAccepted);
        }
    }
}

