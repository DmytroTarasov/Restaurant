using NUnit.Framework;
using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Implementations;
using Persistence.Interfaces;
using Moq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Persistence
{
    [TestFixture]
    public class CategoryRepositoryTests
    {
        private Category _categoryOne;
        private Category _categoryTwo;
        private Mock<IUnitOfWork> _uof;
        private ICategoryRepository _categoryRepository;
        private DataContext _context;

        [SetUp]
        public void Setup() {
            _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "temp_restaurant").Options);

            _uof = new Mock<IUnitOfWork>();
            _categoryRepository = new CategoryRepository(_context);

            _uof.Setup(x => x.CategoryRepository).Returns(_categoryRepository);
            _uof.Setup(x => x.Context).Returns(_context);

            _categoryOne = new Category {
                Id = new Guid("d5110867-8032-47dd-a451-60494595d840"),
                Name = "Grill"
            };

            _categoryTwo = new Category {
                Id = new Guid("db0bc3be-a167-4906-93b2-4f5b989d57aa"),
                Name = "Salads"
            };
        }

        [Test]
        public async Task SaveCategory_CategoryOne_CheckTheValuesFromDatabase() {

            _uof.Object.CategoryRepository.Add(_categoryOne);
            await _uof.Object.Complete();

            var categoryFromDB = await _uof.Object.CategoryRepository.Get(_categoryOne.Id);
            Assert.Multiple(() => {
                Assert.AreEqual(_categoryOne.Id, categoryFromDB.Id);
                Assert.AreEqual(_categoryOne.Name, categoryFromDB.Name);
            });
        }

        [Test]
        public async Task GetById_CategoryNotExist_ReturnsNull() {
            _context.Database.EnsureDeleted();

            _uof.Object.CategoryRepository.Add(_categoryOne);
            await _uof.Object.Complete();

            var result = await _uof.Object.CategoryRepository.Get(new Guid());

            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteById_Category_CheckCategoryNotExist() {
            _context.Database.EnsureDeleted();

            _uof.Object.CategoryRepository.Add(_categoryOne);
            await _uof.Object.Complete();

            _uof.Object.CategoryRepository.Remove(_categoryOne);
            await _uof.Object.Complete();

            var result = await _uof.Object.CategoryRepository.Get(_categoryOne.Id);

            Assert.IsNull(result);
        }

        // [Test]
        // public async Task GetAll_Categories_ReturnsCategories() {
        //     var expectedResult = new List<Category> {_categoryOne, _categoryTwo};
        //     _context.Database.EnsureDeleted();

        //     _uof.Object.CategoryRepository.Add(_categoryOne);
        //     _uof.Object.CategoryRepository.Add(_categoryTwo);
        //     await _uof.Object.Complete();

        //     var result = await _uof.Object.CategoryRepository.GetAll();
        //     Assert.AreEqual(2, result.Count());
        // }
    }
}