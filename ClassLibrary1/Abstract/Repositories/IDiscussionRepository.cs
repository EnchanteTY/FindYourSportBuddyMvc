using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;

namespace FindYourSportBuddy.BL.Abstract.Repositories
{
    public interface IDiscussionRepository
    {
        List<Discussion> GetDiscussionsByEventId(int id);
        void Add(Discussion discussion);
    }
}
