using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.Features.Queries.DistrictQueries.GetByIdDistrict;

namespace WebFotokopi.Application.Features.Queries.PackageQueries.GetPackageFeature
{
    public class GetPackageFeaturesQueryHandler : IRequestHandler<GetPackageFeaturesQueryRequest, GetPackageFeaturesQueryResponse>
    {
        private readonly IPackageService _packageService;

        public GetPackageFeaturesQueryHandler(IPackageService packageService)
        {
            _packageService = packageService;
        }
        public async Task<GetPackageFeaturesQueryResponse> Handle(GetPackageFeaturesQueryRequest request, CancellationToken cancellationToken)
        {
            GetPackageFeaturesDTO packageFeatures = await _packageService.GetPackageFeatureAsync();

            return new GetPackageFeaturesQueryResponse
            {
                PackageFeature = packageFeatures
            };

        }
    }
}
