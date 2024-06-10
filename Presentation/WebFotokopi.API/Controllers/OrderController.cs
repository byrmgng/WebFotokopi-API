using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFotokopi.Application.Features.Commands.OrderCommands.PlaceOrder;
using WebFotokopi.Application.Features.Commands.OrderCommands.UpdateOrderStatus;
using WebFotokopi.Application.Features.Commands.ProductCommands.CreateProductForCustomerCommands;
using WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderDetailsForSellerFilterQueries;
using WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderDetailsForSellerQueries;
using WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderDetailsQueries;
using WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderQueries;

namespace WebFotokopi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IActionResult> GetActiveOrder()
        {
            GetActiveOrderQueryRequest getActiveOrderQueryRequest = new();
            GetActiveOrderQueryRepsonse getActiveOrderQueryRepsonse = await _mediator.Send(getActiveOrderQueryRequest);
            return Ok(getActiveOrderQueryRepsonse);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IActionResult> PlaceOrder()
        {
            PlaceOrderCommandRequest placeOrderCommandRequest = new();
            PlaceOrderCommandResponse placeOrderCommandResponse = await _mediator.Send(placeOrderCommandRequest);
            return Ok(placeOrderCommandResponse);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IActionResult> GetOrderDetails()
        {
            GetOrderDetailsQueryRequest getOrderDetailsQueryRequest = new();
            GetOrderDetailsQueryResponse getOrderDetailsQueryResponse = await _mediator.Send(getOrderDetailsQueryRequest);
            return Ok(getOrderDetailsQueryResponse);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> GetOrderDetailsForSeller()
        {
            GetOrderDetailsForSellerQueryRequest getOrderDetailsQueryRequest = new();
            GetOrderDetailsForSellerQueryResponse getOrderDetailsQueryResponse = await _mediator.Send(getOrderDetailsQueryRequest);
            return Ok(getOrderDetailsQueryResponse);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> GetOrderDetailsForSellerFilter(GetOrderDetailsForSellerFilterQueryRequest getOrderDetailsForSellerFilterQueryRequest)
        {
            GetOrderDetailsForSellerFilterQueryResponse getOrderDetailsForSellerFilterQueryResponse = await _mediator.Send(getOrderDetailsForSellerFilterQueryRequest);
            return Ok(getOrderDetailsForSellerFilterQueryResponse);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusCommandRequest updateOrderStatusCommandRequest)
        {
            UpdateOrderStatusCommandResponse updateOrderStatusCommandResponse = await _mediator.Send(updateOrderStatusCommandRequest);
            return Ok(updateOrderStatusCommandResponse);
        }
    }
}
