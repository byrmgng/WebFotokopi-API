using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderQueries
{
    public class GetActiveOrderQueryHandler : IRequestHandler<GetActiveOrderQueryRequest, GetActiveOrderQueryRepsonse>
    {
        private readonly IOrderService _orderService;
        public GetActiveOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<GetActiveOrderQueryRepsonse> Handle(GetActiveOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _orderService.GetOrderAsync();
            return new()
            {
                Items = response.Items,
                Price = response.Price,
                SellerName = response.SellerName,
            };
        }
    }
}
