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

        public UnitOfWork(DataContext context, IDishRepository dishRepository)
        {
            Context = context;
            DishRepository = dishRepository;
        }
        public void Complete()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}