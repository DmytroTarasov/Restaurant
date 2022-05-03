using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Persistence;
using Persistence.Implementations;
using Persistence.Interfaces;

namespace Tests.ApplicationTests.Dishes
{
    public class CreateDishCommandTests : BaseSetup
    {
        private Mock<IDishRepository> _dishRepository;
        private Mock<ICategoryRepository> _categoryRepository;
        private Dish _dish;
        private List<Dish> _dishes;
        private Category _category;

        [SetUp]
        public new void Setup() {

            _category = new Category { Name = "Category1" };

            var dish =  new Dish { 
                    Id = new Guid(),
                    Name = "Dish1", 
                    Description = "Description1",
                    Category = _category
                };
            
            _dishes = new List<Dish>();
            _dishes.Add(dish);

            _dish = new Dish { 
                Id = new Guid(),
                Name = "Dish2", 
                Description = "Description2",
                Category = _category
            };

            _dishRepository = new Mock<IDishRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();

            _uof.Setup(x => x.DishRepository).Returns(_dishRepository.Object);
            _uof.Setup(x => x.CategoryRepository).Returns(_categoryRepository.Object);

            _dishRepository.Setup(x => x.AddDish(_dish)).Returns((Dish dish) => {
                _dishes.Add(dish);
                return dish;
            });
            _dishRepository.Setup(x => x.GetAll()).Returns(Task.FromResult((IEnumerable<Dish>)_dishes));
        }

        [Test]
        public async Task AddDish_WhenDishIsValid_ReturnsResultUnit() {

            var command = new Application.Dishes.Create.Command { Dish = _dish };

            var handler = new Application.Dishes.Create.Handler(_uof.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            _uof.Verify(x => x.DishRepository.AddDish(It.IsAny<Dish>()), Times.Once);

            Assert.That(result, Is.TypeOf<Result<Unit>>());
            Assert.AreEqual(2, _dishes.Count());
        }
    }
}