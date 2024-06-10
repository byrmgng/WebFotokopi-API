using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.OrderDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderDetailsForSellerQueries
{
    public class GetOrderDetailsForSellerQueryHandler : IRequestHandler<GetOrderDetailsForSellerQueryRequest, GetOrderDetailsForSellerQueryResponse>
    {
        private readonly IOrderService _orderService;
        public GetOrderDetailsForSellerQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<GetOrderDetailsForSellerQueryResponse> Handle(GetOrderDetailsForSellerQueryRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<GetOrderDetailsForSeller> orderDetails = await _orderService.GetOrderDetailsForSellerAsync();
            return new()
            {
                OrderDetails = orderDetails,
            };
        }
    }
}
