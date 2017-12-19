using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;

namespace FindYourSportBuddy.BL.Abstract.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile profile);
        UserProfile GetUserProfileByUserName(string userName);
        List<UserProfile> GetMatchProfile(string profileName, byte categoryId, Region region);
    }
}
