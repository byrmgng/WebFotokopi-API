using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFotokopi.Application.Features.Commands.AppCustomerCommands.CreateCustomer;
using WebFotokopi.Application.Features.Commands.AppCustomerCommands.LoginCustomer;
using WebFotokopi.Application.Features.Commands.AppCustomerCommands.RefreshTokenLoginCustomer;
using WebFotokopi.Application.Features.Commands.AppSellerCommands.CreateSeller;
using WebFotokopi.Application.Features.Commands.AppSellerCommands.LoginSeller;
using WebFotokopi.Application.Features.Commands.AppSellerCommands.RefleshTokenLoginSeller;

namespace WebFotokopi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommandRequest createCustomerCommandRequest)
        {
            CreateCustomerCommandResponse createCustomerCommandResponse = await _mediator.Send(createCustomerCommandRequest);
            return Ok(createCustomerCommandResponse);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginCustomer(LoginCustomerCommandRequest loginCustomerCommandRequest)
        {
            LoginCustomerCommandResponse loginCustomerCommandResponse = await _mediator.Send(loginCustomerCommandRequest);
            return Ok(loginCustomerCommandResponse);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLoginCustomer(RefreshTokenLoginCustomerCommandRequest refreshTokenLoginCustomerCommandRequest)
        {
            RefreshTokenLoginCustomerCommandResponse refreshTokenLoginCustomerCommandResponse = await _mediator.Send(refreshTokenLoginCustomerCommandRequest);
            return Ok(refreshTokenLoginCustomerCommandResponse);
        }
    }
}
