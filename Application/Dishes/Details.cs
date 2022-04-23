using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence.Interfaces;

namespace Application.Dishes
{
    public class Details
    {
        public class Query : IRequest<Result<DishDTO<Guid>>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DishDTO<Guid>>>
        {
            private readonly IUnitOfWork _uof;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork uof, IMapper mapper)
            {
                _uof = uof;
                _mapper = mapper;
            }

            public async Task<Result<DishDTO<Guid>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dish = await _uof.DishRepository.Get(request.Id);

                var dishDTO = _mapper.Map<Dish, DishDTO<Guid>>(dish);
                
                return Result<DishDTO<Guid>>.Success(dishDTO);
            }
        }
    }
}