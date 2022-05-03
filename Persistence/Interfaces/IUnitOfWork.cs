using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public DataContext Context { get; }
        public IDishRepository DishRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public IIngredientRepository IngredientRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public Task<Boolean> Complete();
    }
}