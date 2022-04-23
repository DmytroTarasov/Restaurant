using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence.Interfaces
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
        
    }
}