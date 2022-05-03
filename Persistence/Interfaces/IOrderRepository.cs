using Domain;

namespace Persistence.Interfaces
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        public Task<IEnumerable<Order>> GetAllOrdersWithRelatedEntities();

        // public Task<Order> GetByIdWithRelatedEntities(Guid id);
        public Order AddOrder(Order order);
    }
}