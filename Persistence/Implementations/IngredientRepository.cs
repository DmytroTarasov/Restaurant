using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Persistence.Interfaces;

namespace Persistence.Implementations
{
    public class IngredientRepository : Repository<Ingredient, Guid>, IIngredientRepository
    {
        public IngredientRepository(DataContext context) : base(context)
        {
        }
    }
}