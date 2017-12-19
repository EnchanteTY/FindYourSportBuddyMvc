using FindYourSportBuddy.BL.Abstract.Repositories;
using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FindYourSportBuddy.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;

        }


        public List<Category> Categories { get { return _context.Categories.ToList(); } }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.SingleOrDefault(c => c.Id == id);
        }

    }
}
