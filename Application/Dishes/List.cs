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
    public class List
    {
        public class Query : IRequest<Result<List<DishDTO<Guid>>>> {}
        public class Handler : IRequestHandler<Query, Result<List<DishDTO<Guid>>>>
        {
            private readonly IUnitOfWork _uof;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork uof, IMapper mapper)
            {
                _uof = uof;
                _mapper = mapper;
            }
            public async Task<Result<List<DishDTO<Guid>>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dishes = await _uof.DishRepository.GetAll();

                var dishesDTO = _mapper.Map<List<Dish>, List<DishDTO<Guid>>>(dishes.ToList());

                return Result<List<DishDTO<Guid>>>.Success(dishesDTO);
            }
        }
    }
}