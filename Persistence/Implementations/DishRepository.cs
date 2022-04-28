using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Implementations
{
    public class DishRepository : Repository<Dish, Guid>, IDishRepository
    {
        public DishRepository(DataContext context) : base(context) { }

        public void AddDish(Dish dish)
        {
            Context.Entry(dish.Category).State = EntityState.Unchanged;
            dish.Ingredients.ToList().ForEach(i => Context.Entry(i).State = EntityState.Unchanged);
            Context.Dishes.Add(dish);
        }

        public async Task<IEnumerable<Dish>> GetAllDishesWithRelatedEntities()
        {
            return await Context.Dishes
                .Include(d => d.Portions)
                .Include(d => d.Photo)
                .Include(d => d.Ingredients)
                .ToListAsync();
        }
    }
}