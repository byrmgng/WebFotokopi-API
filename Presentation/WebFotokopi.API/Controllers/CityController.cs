using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFotokopi.Application.Features.Queries.CityQueries.GetAllCity;

namespace WebFotokopi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        readonly IMediator _mediator;
        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCity()
        {
            GetAllCityQueryRequest getAllCityQueryRequest = new();
            GetAllCityQueryResponse getAllCityQueryResponse = await _mediator.Send(getAllCityQueryRequest);
            return Ok(getAllCityQueryResponse);
        }

    }
}
