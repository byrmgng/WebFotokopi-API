using MediatR;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.Features.Queries.CityQueries.GetAllCity;

namespace WebFotokopi.Application.Features.Queries.DistrictQueries.GetByIdDistrict
{
    public class GetByCityIdDistrictQueryHandler : IRequestHandler<GetByCityIdDistrictQueryRequest, GetByCityIdDistrictQueryResponse>
    {
        private readonly IDistrictService _districtService;

        public GetByCityIdDistrictQueryHandler(IDistrictService districtService)
        {
            _districtService = districtService;
        }
        public async Task<GetByCityIdDistrictQueryResponse> Handle(GetByCityIdDistrictQueryRequest request, CancellationToken cancellationToken)
        {
            var districts = await _districtService.GetByCityIdDistrictAsync(request.CityID);

            return new GetByCityIdDistrictQueryResponse
            {
                Districts = districts
            };
        }
    }
}
