using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Ingredients;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class IngredientsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllIngredients() {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
    }
}