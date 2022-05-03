using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Ingredients;
using Domain;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;

namespace Tests.ApplicationTests
{
    public class ListIngredientsQueryTests : BaseSetup
    {
        private Mock<IIngredientRepository> _ingredientRepository;
        private List<Ingredient> _ingredients;

        [SetUp]
        public new void Setup() {
            _ingredients = new List<Ingredient> {
                new Ingredient { Name = "Ingredient1" },
                new Ingredient { Name = "Ingredient2" },
                new Ingredient { Name = "Ingredient3" }
            };

            _ingredientRepository = new Mock<IIngredientRepository>();

            _uof.Setup(x => x.IngredientRepository).Returns(_ingredientRepository.Object);
            _ingredientRepository.Setup(x => x.GetAll()).Returns(Task.FromResult((IEnumerable<Ingredient>) _ingredients));
        }

        [Test]
        public async Task GetAllIngredients_WhenIngredientsExist_ReturnsIngredientsCollection() {

            var query = new Application.Ingredients.List.Query();
            var handler = new Application.Ingredients.List.Handler(_uof.Object, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            _uof.Verify(x => x.IngredientRepository.GetAll(), Times.Once);   
            Assert.That(result, Is.TypeOf<Result<List<IngredientDTO<Guid>>>>());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(3, result.Value.Count);
        }
    }
}