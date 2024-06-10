using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.OrderDTOs;

namespace WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderDetailsForSellerFilterQueries
{
    public class GetOrderDetailsForSellerFilterQueryHandler : IRequestHandler<GetOrderDetailsForSellerFilterQueryRequest, GetOrderDetailsForSellerFilterQueryResponse>
    {
        private readonly IOrderService _orderService;
        public GetOrderDetailsForSellerFilterQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<GetOrderDetailsForSellerFilterQueryResponse> Handle(GetOrderDetailsForSellerFilterQueryRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<GetOrderDetailsForSeller> orderDetails = await _orderService.GetOrderDetailsForSellerFilterAsync(new() { FilterProductStatus = request.FilterProductStatus, FilterCustomerPhoneNumber =request.FilterCustomerPhoneNumber,FilterCustomerName=request.FilterCustomerName }); ;
            return new()
            {
                OrderDetails = orderDetails,
            };
        }
    }
}
