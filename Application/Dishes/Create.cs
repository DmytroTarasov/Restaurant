using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Persistence;
using Persistence.Interfaces;

namespace Application.Dishes
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Dish Dish { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUnitOfWork _uof;
            public Handler(IUnitOfWork uof) {
                _uof = uof;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await _uof.CategoryRepository.Get(request.Dish.Category.Id);
                var dish = request.Dish;
                dish.Category = category;

                _uof.DishRepository.Add(dish);
                var result = await _uof.Complete();

                if (!result) return Result<Unit>.Failure("Failed to create a dish");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}