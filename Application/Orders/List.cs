using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence.Interfaces;

namespace Application.Orders
{
    public class List
    {
       public class Query : IRequest<Result<List<OrderDTO<Guid>>>> {}
        public class Handler : IRequestHandler<Query, Result<List<OrderDTO<Guid>>>>
        {
            private readonly IUnitOfWork _uof;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uof, IMapper mapper)
            {
                _uof = uof;
                _mapper = mapper;
            }

            public async Task<Result<List<OrderDTO<Guid>>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var orders = await _uof.OrderRepository.GetAllOrdersWithRelatedEntities();

                return Result<List<OrderDTO<Guid>>>.Success(_mapper.Map<List<Order>, List<OrderDTO<Guid>>>(orders.ToList()));
            }
        } 
    }
}