using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Implementations
{
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Dish>> GetAllCategoryDishes(string categoryName)
        {
            return await Context.Dishes
                .Include(d => d.Category)
                .Include(d => d.Portions)
                .Include(d => d.Photo)
                .Include(d => d.Ingredients)
                .Where(d => d.Category.Name == categoryName).ToListAsync();
        }
    }
}