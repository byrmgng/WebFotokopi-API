using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.Features.Queries.PackageQueries.GetAllPackage
{
    public class GetAllPackageQueryHandler : IRequestHandler<GetAllPackageQueryRequest, GetAllPackageQueryResponse>
    {
        private readonly IPackageService _packageService;

        public GetAllPackageQueryHandler(IPackageService packageService)
        {
            _packageService = packageService;
        }
        public async Task<GetAllPackageQueryResponse> Handle(GetAllPackageQueryRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<GetPackageDTO> packageDTOs = await _packageService.GetAllPackageAsync();
            return new()
            {
                Packages = packageDTOs,
            };
        }
    }
}
