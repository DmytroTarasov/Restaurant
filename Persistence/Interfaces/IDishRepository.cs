using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence.Interfaces
{
    public interface IDishRepository : IRepository<Dish, Guid>
    {
        public Task<IEnumerable<Dish>> GetAllDishesWithRelatedEntities();
        public Dish AddDish(Dish dish);
    }
}