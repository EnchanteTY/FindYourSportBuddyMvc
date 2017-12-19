using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;

namespace FindYourSportBuddy.BL.Abstract.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> Categories { get; }
        Category GetCategoryById(int id);
    }
}
