using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.ViewModels.Package;

namespace WebFotokopi.Application.Features.Queries.PackageQueries.FilterGetPackage
{
    public class GetFilterPackageQueryHandler : IRequestHandler<GetFilterPackageQueryRequest, GetFilterPackageQueryResponse>
    {
        private readonly IPackageService _packageService;

        public GetFilterPackageQueryHandler(IPackageService packageService)
        {
            _packageService = packageService;
        }
        public async Task<GetFilterPackageQueryResponse> Handle(GetFilterPackageQueryRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<GetPackageDTO> packageDTOs = await _packageService.GetFilterPackageAsync(new()
            {
                ColorMode = request.ColorMode,
                DuplexMode = request.DuplexMode,
                IsActive = request.IsActive,
                SheetsPerPageID = request.SheetsPerPageID,
                PaperSizeID = request.PaperSizeID,
                PaperTypeID = request.PaperTypeID,
                PackageName = request.PackageName,
            });
            return new()
            {
                Packages = packageDTOs,
            };
        }
    }
}
