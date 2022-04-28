using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence.Interfaces;

namespace Application.Ingredients
{
    public class List
    {
        public class Query : IRequest<Result<List<IngredientDTO<Guid>>>> {}

        public class Handler : IRequestHandler<Query, Result<List<IngredientDTO<Guid>>>>
        {
            private readonly IUnitOfWork _uof;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uof, IMapper mapper)
            { 
                _uof = uof;
                _mapper = mapper;
            }

            public async Task<Result<List<IngredientDTO<Guid>>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ingredients = await _uof.IngredientRepository.GetAll();

                var ingredientsDTO = _mapper.Map<List<Ingredient>, List<IngredientDTO<Guid>>>(ingredients.ToList());

                return Result<List<IngredientDTO<Guid>>>.Success(ingredientsDTO);
            }
        }
    }
}