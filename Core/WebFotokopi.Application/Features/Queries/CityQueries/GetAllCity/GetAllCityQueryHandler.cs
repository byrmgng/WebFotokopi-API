using MediatR;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.CityDTOs;


namespace WebFotokopi.Application.Features.Queries.CityQueries.GetAllCity
{
    public class GetAllCityQueryHandler : IRequestHandler<GetAllCityQueryRequest, GetAllCityQueryResponse>
    {
        private readonly ICityService _cityService;

        public GetAllCityQueryHandler(ICityService cityService)
        {
            _cityService = cityService;
        }

        public async Task<GetAllCityQueryResponse> Handle(GetAllCityQueryRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<ListCityDTO> cities = await _cityService.GetAllCityAsync();

            return new GetAllCityQueryResponse
            {
                Cities = cities
            };
        }

    }
}
