using FindYourSportBuddy.BL.Abstract.Repositories;
using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FindYourSportBuddy.DataAccess.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private AppDbContext _context;
        public UserProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(UserProfile profile)
        {
            _context.UserProfiles.Add(profile);

        }

        public UserProfile GetUserProfileByUserName(string userName)
        {
            return _context.UserProfiles.SingleOrDefault(p => p.Name == userName); ;
        }

        public List<UserProfile> GetMatchProfile(string profileName, byte categoryId, Region region)
        {
            return _context.UserProfiles
                .Where(p => p.Name != profileName && p.InterestedSportId == categoryId && p.PreferSportRegion == region)
                .ToList();
        }
    }
}
