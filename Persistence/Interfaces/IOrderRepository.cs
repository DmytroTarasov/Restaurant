using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence.Interfaces
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        public Task<IEnumerable<Order>> GetAllOrdersWithRelatedEntities();

        // public Task<Order> GetByIdWithRelatedEntities(Guid id);
        public void AddOrder(Order order);
    }
}