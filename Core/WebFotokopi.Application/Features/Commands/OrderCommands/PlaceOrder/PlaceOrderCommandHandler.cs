using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.OrderDTOs;

namespace WebFotokopi.Application.Features.Commands.OrderCommands.PlaceOrder
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommandRequest, PlaceOrderCommandResponse>
    {
        private readonly IOrderService _orderService;

        public PlaceOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<PlaceOrderCommandResponse> Handle(PlaceOrderCommandRequest request, CancellationToken cancellationToken)
        {
            PlaceOrderDTO placeOrder = await _orderService.PlaceOrderAsync();
            return new() { Message = placeOrder.Message, Succeeded = placeOrder.Succeeded };
        }
    }
}
