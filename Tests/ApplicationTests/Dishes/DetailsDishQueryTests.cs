using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Dishes;
using Domain;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;

namespace Tests.ApplicationTests.Dishes
{
    public class DetailsDishQueryTests : BaseSetup
    {
        private Mock<IDishRepository> _dishRepository;
        private List<Dish> _dishes;

        [SetUp]
        public new void Setup() {

            _dishes = new List<Dish> {
                new Dish { Id = new Guid("6c98c723-2ca5-49ba-9b7e-168aa52ccb27"), Name = "Dish1", Description = "Description1" },
                new Dish { Id = new Guid("9ee32f58-6ef1-483f-a64f-7de3a2ceef1e"), Name = "Dish2", Description = "Description2" },
                new Dish { Id = new Guid("a00527a1-9d3c-4838-bf20-b4787b87cbd5"), Name = "Dish3", Description = "Description3" }
            };

            _dishRepository = new Mock<IDishRepository>();

            _uof.Setup(x => x.DishRepository).Returns(_dishRepository.Object);
        }

        [Test]
        public async Task GetDishById_WhenDishExists_ReturnsDish() {
            _dishRepository.Setup(x => x.Get(_dishes.First().Id))
                .Returns(Task.FromResult(_dishes.First()));

            var query = new Application.Dishes.Details.Query { Id = _dishes.First().Id};
            var handler = new Application.Dishes.Details.Handler(_uof.Object, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            _uof.Verify(x => x.DishRepository.Get(It.IsAny<Guid>()), Times.Once);
               
            Assert.That(result, Is.TypeOf<Result<DishDTO<Guid>>>());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(_dishes.First().Id, result.Value.Id);
            Assert.AreEqual(_dishes.First().Name, result.Value.Name);
            Assert.AreEqual(_dishes.First().Description, result.Value.Description);
        }

        [Test]
        public async Task GetDishById_WhenDishNotExists_ReturnsNull() {
            _dishRepository.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var query = new Application.Dishes.Details.Query { Id = new Guid() };
            var handler = new Application.Dishes.Details.Handler(_uof.Object, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            _uof.Verify(x => x.DishRepository.Get(It.IsAny<Guid>()), Times.Once);
               
            Assert.That(result, Is.TypeOf<Result<DishDTO<Guid>>>());
            Assert.IsNull(result.Value);
        }
    }
}