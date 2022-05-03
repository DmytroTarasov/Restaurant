using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence;
using Persistence.Interfaces;

namespace Persistence.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public DataContext Context { get; }
        public IDishRepository DishRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public IIngredientRepository IngredientRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public UnitOfWork(DataContext context, 
            IDishRepository dishRepository, 
            ICategoryRepository categoryRepository,
            IIngredientRepository ingredientRepository,
            IOrderRepository orderRepository)
        {
            Context = context;
            DishRepository = dishRepository;
            CategoryRepository = categoryRepository;
            IngredientRepository = ingredientRepository;
            OrderRepository = orderRepository;
        }
        public async Task<Boolean> Complete()
        {
            return await Context.SaveChangesAsync() > 0;
        }
        // public async ValueTask DisposeAsync()
        // {
        //     await Context.DisposeAsync(); // maybe, need a call to GC.SuppressFinalize(this)
        // }
        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}