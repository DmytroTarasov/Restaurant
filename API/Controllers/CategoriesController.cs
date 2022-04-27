using Application.Categories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCategories() {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
    }
}