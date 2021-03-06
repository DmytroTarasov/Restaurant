using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Implementations
{
    public class OrderRepository : Repository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context) {}
        public Order AddOrder(Order order)
        {
            Context.Entry(order.User).State = EntityState.Unchanged;
            order.Portions.ToList().ForEach(p => Context.Entry(p).State = EntityState.Unchanged);
            Context.Orders.Add(order);
            return order;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersWithRelatedEntities()
        {
            return await Context.Orders
                .Include(o => o.User)
                .Include(o => o.Portions)
                .ThenInclude(p => p.Dish)
                .OrderBy(x => x.Date)
                .ToListAsync();
        }
    }
}