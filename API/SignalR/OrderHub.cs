using Application.Orders;
using Domain;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class OrderHub : Hub
    {
        private readonly IMediator _mediator;

        public OrderHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendOrder(Order order) {
            // Console.WriteLine("ORDER: " + order);
            // Console.WriteLine(order.User.DisplayName);
            var orderDTO = await _mediator.Send(new Create.Command { Order = order });

            await Clients.All.SendAsync("ReceiveOrder", orderDTO.Value);
        }

        public override async Task OnConnectedAsync() {

            var result = await _mediator.Send(new List.Query());
            
            await Clients.Caller.SendAsync("LoadOrders", result.Value);
        }
    }
}