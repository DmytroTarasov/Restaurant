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

        // public async Task<IEnumerable<Dish>> GetAllDishesWithCategory()
        // {
        //     return await Context.Dishes.Include(d => d.Category).ToListAsync();
        // }
    }
}