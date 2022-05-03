using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Categories;
using Application.Core;
using AutoMapper;
using Domain;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;

namespace Tests.ApplicationTests
{
    public class ListCategoriesQueryTests : BaseSetup
    {
        private Mock<ICategoryRepository> _categoryRepository;
        private List<Category> _categories;

        [SetUp]
        public new void Setup() {

            _categories = new List<Category> {
                new Category { Name = "Grill" },
                new Category { Name = "Salads" },
                new Category { Name = "Soups" }
            };

            _categoryRepository = new Mock<ICategoryRepository>();

            _uof.Setup(x => x.CategoryRepository).Returns(_categoryRepository.Object);
            _categoryRepository.Setup(x => x.GetAll()).Returns(Task.FromResult((IEnumerable<Category>)_categories));
        }

        [Test]
        public async Task GetAllCategories_WhenCategoriesExist_ReturnsCategoriesCollection() {

            var query = new Application.Categories.List.Query();
            var handler = new Application.Categories.List.Handler(_uof.Object, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            _uof.Verify(x => x.CategoryRepository.GetAll(), Times.Once);   
            Assert.That(result, Is.TypeOf<Result<List<CategoryDTO<Guid>>>>());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(3, result.Value.Count);
        }

        // [Test]
        // public async Task TestMethod() {
        //     Assert.That(_categories, Is.EquivalentTo(await _categoryRepository.Object.GetAll()));
        // }
    }
}