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
    public class ListDishesQueryTests : BaseSetup
    {
        private Mock<IDishRepository> _dishRepository;
        private Mock<ICategoryRepository> _categoryRepository;
        private List<Dish> _dishes;
        private List<Category> _categories;

        [SetUp]
        public new void Setup() {
            _categories = new List<Category> {
                new Category { Name = "Grill" },
                new Category { Name = "Salads" },
                new Category { Name = "Soups" }
            };

            _dishes = new List<Dish> {
                new Dish { Name = "Dish1", Category = _categories[0] },
                new Dish { Name = "Dish2", Category = _categories[1] },
                new Dish { Name = "Dish3", Category = _categories[2] }
            };

            _dishRepository = new Mock<IDishRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();

            _uof.Setup(x => x.DishRepository).Returns(_dishRepository.Object);
            _uof.Setup(x => x.CategoryRepository).Returns(_categoryRepository.Object);

        }

        [TestCase("All", ExpectedResult = 3)]
        [TestCase("Salads", ExpectedResult = 1)]
        public async Task<int> GetFilteredDishesByCategory_WhenDishesExist_ReturnsDishesCollection(string categoryName) {
            _categoryRepository.Setup(x => x.GetAllCategoryDishes(categoryName))
                .Returns(Task.FromResult((IEnumerable<Dish>) _dishes.Where(d => d.Category.Name == categoryName)));
            _dishRepository.Setup(x => x.GetAllDishesWithRelatedEntities())
                .Returns(Task.FromResult((IEnumerable<Dish>) _dishes));

            var query = new Application.Dishes.List.Query();
            query.Params = new DishParams { CategoryName = categoryName };
            var handler = new Application.Dishes.List.Handler(_uof.Object, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            _uof.Verify(x => x.CategoryRepository.GetAllCategoryDishes(categoryName), Times.AtMostOnce);   
            _uof.Verify(x => x.DishRepository.GetAllDishesWithRelatedEntities(), Times.AtMostOnce);
               
            Assert.That(result, Is.TypeOf<Result<List<DishDTO<Guid>>>>());
            Assert.IsTrue(result.IsSuccess);
            
            return result.Value.Count;
        }
    }
}