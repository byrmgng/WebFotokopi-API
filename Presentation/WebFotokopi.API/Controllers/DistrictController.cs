using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFotokopi.Application.Features.Queries.CityQueries.GetAllCity;
using WebFotokopi.Application.Features.Queries.DistrictQueries.GetByIdDistrict;

namespace WebFotokopi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        readonly IMediator _mediator;
        public DistrictController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetByCityIdDistrict(int cityId)
        {
            GetByCityIdDistrictQueryRequest getByCityIdDistrictQueryRequest = new(){CityID = cityId};
            GetByCityIdDistrictQueryResponse getByCityIdDistrictQueryResponse = await _mediator.Send(getByCityIdDistrictQueryRequest);
            return Ok(getByCityIdDistrictQueryResponse);
        }
    }
}
