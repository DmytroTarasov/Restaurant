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

        public UnitOfWork(DataContext context, IDishRepository dishRepository, ICategoryRepository categoryRepository)
        {
            Context = context;
            DishRepository = dishRepository;
            CategoryRepository = categoryRepository;
        }
        public async Task Complete()
        {
            await Context.SaveChangesAsync();
        }
        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync(); // maybe, need a call to GC.SuppressFinalize(this)
        }
    }
}