using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Orders;
using Domain;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;

namespace Tests.ApplicationTests.Orders
{
    public class ListOrdersQueryTests : BaseSetup
    {
        private Mock<IOrderRepository> _orderRepository;
        private List<Order> _orders;

        [SetUp]
        public new void Setup() {
            _orders = new List<Order> {
                new Order()
            };

            _orderRepository = new Mock<IOrderRepository>();

            _uof.Setup(x => x.OrderRepository).Returns(_orderRepository.Object);
            _orderRepository.Setup(x => x.GetAllOrdersWithRelatedEntities())
                .Returns(Task.FromResult((IEnumerable<Order>) _orders));
        }

        [Test]
        public async Task GetAllOrders_WhenOrdersExist_ReturnsOrdersCollection() {
            var query = new Application.Orders.List.Query();
            var handler = new Application.Orders.List.Handler(_uof.Object, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            _uof.Verify(x => x.OrderRepository.GetAllOrdersWithRelatedEntities(), Times.Once);   
            Assert.That(result, Is.TypeOf<Result<List<OrderDTO<Guid>>>>());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.Value.Count);
        }

        
        [Test]
        public async Task GetAllOrders_WhenOrdersNotExist_ReturnsEmptyCollection() {
            _orders.Clear();

            var query = new Application.Orders.List.Query();
            var handler = new Application.Orders.List.Handler(_uof.Object, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            _uof.Verify(x => x.OrderRepository.GetAllOrdersWithRelatedEntities(), Times.Once);   
            Assert.That(result, Is.TypeOf<Result<List<OrderDTO<Guid>>>>());
            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(result.Value);
        }
    }
}