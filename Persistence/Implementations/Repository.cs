using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Implementations
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected readonly DataContext Context;
        protected Repository(DataContext context) {
            Context = context;
        }
        public async Task<TEntity> Get(TKey id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }
    }
}