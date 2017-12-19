using FindYourSportBuddy.BL.Abstract.Repositories;
using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FindYourSportBuddy.DataAccess.Repositories
{
    public class DiscussionRepository : IDiscussionRepository
    {
        private AppDbContext _context;
        public DiscussionRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Discussion> GetDiscussionsByEventId(int id)
        {
            return _context.Discussions.Where(d => d.EventId == id).ToList();
        }

        public void Add(Discussion discussion)
        {
            _context.Discussions.Add(discussion);
        }
    }
}
