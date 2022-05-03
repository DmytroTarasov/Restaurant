using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence.Interfaces;

namespace Application.Categories
{
    public class List
    {
        public class Query : IRequest<Result<List<CategoryDTO<Guid>>>> {}
        public class Handler : IRequestHandler<Query, Result<List<CategoryDTO<Guid>>>>
        {
            private readonly IUnitOfWork _uof;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork uof, IMapper mapper)
            {
                _uof = uof;
                _mapper = mapper;
            }
            public async Task<Result<List<CategoryDTO<Guid>>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var categories = await _uof.CategoryRepository.GetAll();
                var categoriesDTO = _mapper.Map<List<Category>, List<CategoryDTO<Guid>>>(categories.ToList());

                return Result<List<CategoryDTO<Guid>>>.Success(categoriesDTO);
            }
        }
    }
}