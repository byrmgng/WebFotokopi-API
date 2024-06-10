using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFotokopi.Application.Features.Commands.PackageCommands.UpdatePackageCommands;
using WebFotokopi.Application.Features.Commands.ProductCommands.CreateProductCommands;
using WebFotokopi.Application.Features.Commands.ProductCommands.CreateProductForCustomerCommands;
using WebFotokopi.Application.Features.Commands.ProductCommands.DeleteProductCommands;

namespace WebFotokopi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse createProductCommandResponse = await _mediator.Send(createProductCommandRequest);
            return Ok(createProductCommandResponse);
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IActionResult> CreateProductForCustomer(CreateProductForCustomerCommandRequest createProductForCustomerCommandRequest)
        {
            CreateProductForCustomerCommandResponse createProductForCustomerCommandResponse = await _mediator.Send(createProductForCustomerCommandRequest);
            return Ok(createProductForCustomerCommandResponse);
        }
        [HttpDelete("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            DeleteProductCommandRequest deleteProductCommandRequest = new() { ProductID = id };
            DeleteProductCommandResponse deleteProductCommandResponse = await _mediator.Send(deleteProductCommandRequest);
            return Ok(deleteProductCommandResponse);
        }
    }
}
