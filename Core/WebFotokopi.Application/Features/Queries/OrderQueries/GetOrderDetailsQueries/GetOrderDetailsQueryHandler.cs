using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.OrderDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderDetailsQueries
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQueryRequest, GetOrderDetailsQueryResponse>
    {
        private readonly IOrderService _orderService;
        public GetOrderDetailsQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<GetOrderDetailsQueryResponse> Handle(GetOrderDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<GetOrderDetails> orderDetails = await _orderService.GetOrderDetailsAsync();
            return new()
            {
                OrderDetails = orderDetails,
            };
        }
    }
}
