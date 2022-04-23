using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dishes;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DishesController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDish(Guid id) {
            return HandleResult(await Mediator.Send(new Details.Query{ Id = id })); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDishes() {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
    }
}