using FindYourSportBuddy.BL.Abstract.Repositories;
using System.Linq;

namespace FindYourSportBuddy.DataAccess.Repositories
{
    public class FollowingRepository : IFollowingRespository
    {
        private readonly AppDbContext _context;

        public FollowingRepository(AppDbContext context)
        {
            _context = context;
        }


        public bool CheckIsFollowing(string friendId, string userId)
        {
            return _context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == friendId);
        }
    }
}
