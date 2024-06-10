using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFotokopi.Application.Features.Commands.AppSellerCommands.CreateSeller;
using WebFotokopi.Application.Features.Commands.PackageCommands.AddPackageCommands;
using WebFotokopi.Application.Features.Commands.PackageCommands.DeletePackageCommands;
using WebFotokopi.Application.Features.Commands.PackageCommands.UpdatePackageCommands;
using WebFotokopi.Application.Features.Queries.CityQueries.GetAllCity;
using WebFotokopi.Application.Features.Queries.PackageQueries.FilterGetPackage;
using WebFotokopi.Application.Features.Queries.PackageQueries.GetAllPackage;
using WebFotokopi.Application.Features.Queries.PackageQueries.GetFilterPackageCustomer;
using WebFotokopi.Application.Features.Queries.PackageQueries.GetPackageFeature;

namespace WebFotokopi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        readonly IMediator _mediator;

        public PackageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPackageFeatures()
        {
            GetPackageFeaturesQueryRequest getPackageFeaturesQueryRequest = new();
            GetPackageFeaturesQueryResponse getPackageFeaturesQueryResponse = await _mediator.Send(getPackageFeaturesQueryRequest);
            return Ok(getPackageFeaturesQueryResponse);
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> CreatePackage(CreatePackageCommandRequest createPackageCommandRequest)
        {
            CreatePackageCommandResponse createPackageCommandResponse = await _mediator.Send(createPackageCommandRequest);
            return Ok(createPackageCommandResponse);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> GetAllPackage()
        {
            GetAllPackageQueryRequest getAllPackageQueryRequest = new();
            GetAllPackageQueryResponse getAllPackageQueryResponse = await _mediator.Send(getAllPackageQueryRequest);
            return Ok(getAllPackageQueryResponse);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> GetFilterPackage(GetFilterPackageQueryRequest filterGetPackageQueryRequest)
        {
            GetFilterPackageQueryResponse filterGetPackageQueryResponse = await _mediator.Send(filterGetPackageQueryRequest);
            return Ok(filterGetPackageQueryResponse);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IActionResult> GetFilterPackageCustomer(GetFilterPackageCustomerQueryRequest getFilterPackageCustomerRequest)
        {
            GetFilterPackageCustomerQueryResponse getFilterPackageCustomerQueryResponse = await _mediator.Send(getFilterPackageCustomerRequest);
            return Ok(getFilterPackageCustomerQueryResponse);
        }
        [HttpDelete("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> DeletePackage(string id)
        {
            DeletePackageCommandsRequest deletePackageCommandsRequest = new() { PackageID = id};
            DeletePackageCommandsResponse deletePackageCommandsResponse = await _mediator.Send(deletePackageCommandsRequest);
            return Ok(deletePackageCommandsResponse);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> UpdatePackage(UpdatePackageCommandsRequest updatePackageCommandsRequest)
        {
            UpdatePackageCommandsResponse updatePackageCommandsResponse = await _mediator.Send(updatePackageCommandsRequest);
            return Ok(updatePackageCommandsResponse);
        }

    }
}
