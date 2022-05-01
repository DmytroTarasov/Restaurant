using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dishes;
using Application.Photos;
using Domain;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetDishes([FromQuery] DishParams dishParams) {
            return HandleResult(await Mediator.Send(
                new Application.Dishes.List.Query { Params = dishParams }));
        }
        
        [HttpPost]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> CreateDish([FromBody]Dish dish) {
            return HandleResult(await Mediator.Send(new Create.Command{ Dish = dish }));
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpPost("{id}/addPhoto")]
        public async Task<IActionResult> AddPhoto([FromForm] AddDishPhoto.Command command, string id) {
            command.DishId = Guid.Parse(id);
            return HandleResult(await Mediator.Send(command));
        }
    }
}