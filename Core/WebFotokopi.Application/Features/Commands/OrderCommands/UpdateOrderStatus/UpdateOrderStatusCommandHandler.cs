using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.OrderDTOs;

namespace WebFotokopi.Application.Features.Commands.OrderCommands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommandRequest, UpdateOrderStatusCommandResponse>
    {
        private readonly IOrderService _orderService;

        public UpdateOrderStatusCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<UpdateOrderStatusCommandResponse> Handle(UpdateOrderStatusCommandRequest request, CancellationToken cancellationToken)
        {
            UpdateOrderStatusDTO updateOrderStatus = await _orderService.UpdateOrderStatusAsync(new() { OrderID=request.OrderID,Status=request.Status});
            return new() { Message = updateOrderStatus.Message, Succeeded = updateOrderStatus.Succeeded };
        }
    }
}
