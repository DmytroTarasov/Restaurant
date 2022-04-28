using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IDishRepository DishRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public IIngredientRepository IngredientRepository { get; set; }
        public Task<Boolean> Complete();
    }
}