using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Persistence.Interfaces;

namespace Application.Orders
{
    public class Create
    {
        public class Command : IRequest<Result<OrderDTO<Guid>>>
        {
            public Order Order { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<OrderDTO<Guid>>>
        {
            private readonly IUnitOfWork _uof;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork uof, IMapper mapper) 
            {    
                _uof = uof;
                _mapper = mapper;
            }
            public async Task<Result<OrderDTO<Guid>>> Handle(Command request, CancellationToken cancellationToken)
            {
                _uof.OrderRepository.AddOrder(request.Order);
                var result = await _uof.Complete();

                if (!result) return Result<OrderDTO<Guid>>.Failure("Failed to create an order");

                return Result<OrderDTO<Guid>>.Success(_mapper.Map<Order, OrderDTO<Guid>>(request.Order));
            }
        }
    }
}