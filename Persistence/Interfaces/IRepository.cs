using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Task<TEntity> Get(TKey id);
        public Task<IEnumerable<TEntity>> GetAll();

    }
}