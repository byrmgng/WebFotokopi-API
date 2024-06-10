using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFotokopi.Application.Features.Commands.AppSellerCommands.CreateSeller;
using WebFotokopi.Application.Features.Commands.AppSellerCommands.GetSellerAccountInfo;
using WebFotokopi.Application.Features.Commands.AppSellerCommands.LoginSeller;
using WebFotokopi.Application.Features.Commands.AppSellerCommands.RefleshTokenLoginSeller;
using WebFotokopi.Application.Features.Commands.AppSellerCommands.UpdateSeller;
using WebFotokopi.Application.Features.Commands.AppSellerCommands.UpdateSellerLogo;
using WebFotokopi.Application.Features.Commands.AppSellerCommands.UpdateSellerLogo2;
using WebFotokopi.Application.Features.Commands.SellerAddressCommands.UpdateSellerAddress;
using WebFotokopi.Application.Features.Queries.PackageQueries.FilterGetPackage;
using WebFotokopi.Application.Features.Queries.SellerQueries.FilterGetSeller;
using WebFotokopi.Application.Features.Queries.SellerQueries.GetSellerFeatures;

namespace WebFotokopi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        readonly IMediator _mediator;

        public SellerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSeller(CreateSellerCommandRequest createSellerCommandRequest)
        {
            CreateSellerCommandResponse createSellerCommandResponse = await _mediator.Send(createSellerCommandRequest);
            return Ok(createSellerCommandResponse);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginSeller(LoginSellerCommandRequest loginSellerCommandRequest)
        {
            LoginSellerCommandResponse loginSellerCommandResponse = await _mediator.Send(loginSellerCommandRequest);
            return Ok(loginSellerCommandResponse);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLoginSeller(RefreshTokenLoginSellerCommandRequest refreshTokenLoginSellerCommandRequest)
        {
            RefreshTokenLoginSellerCommandResponse refreshTokenLoginSellerCommandResponse = await _mediator.Send(refreshTokenLoginSellerCommandRequest);
            return Ok(refreshTokenLoginSellerCommandResponse);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IActionResult> GetFilterSeller(FilterGetSellerQueryRequest filterGetSellerQueryRequest)
        {
            FilterGetSellerQueryResponse filterGetSellerQueryResponse = await _mediator.Send(filterGetSellerQueryRequest);
            return Ok(filterGetSellerQueryResponse);
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IActionResult> GetSellerFeatures(GetSellerFeaturesRequest getSellerFeaturesRequest)
        {
            GetSellerFeaturesResponse getSellerFeaturesResponse = await _mediator.Send(getSellerFeaturesRequest);
            return Ok(getSellerFeaturesResponse);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> GetSellerAccountInfo()
        {
            GetSellerAccountInfoCommandRequest request = new();
            GetSellerAccountInfoCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> UpdateSellerAddress(UpdateSellerAddressCommandRequest request)
        {
            UpdateSellerAddressCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> UpdateSeller(UpdateSellerCommandRequest request)
        {
            UpdateSellerCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> UpdateSellerLogo([FromForm] UpdateSellerLogoCommandRequest request)
        {
            UpdateSellerLogoCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> UpdateSellerLogo2([FromForm] UpdateSellerLogo2CommandRequest request)
        {
            UpdateSellerLogo2CommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }


    }
}
