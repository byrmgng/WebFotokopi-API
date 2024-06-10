using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.Features.Queries.PackageQueries.GetFilterPackageCustomer
{
    public class GetFilterPackageCustomerQueryHandler : IRequestHandler<GetFilterPackageCustomerQueryRequest, GetFilterPackageCustomerQueryResponse>
    {
        private readonly IPackageService _packageService;

        public GetFilterPackageCustomerQueryHandler(IPackageService packageService)
        {
            _packageService = packageService;
        }
        public async Task<GetFilterPackageCustomerQueryResponse> Handle(GetFilterPackageCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<GetCustomersPackageDTO> packageDTOs = await _packageService.GetFilterCustomerPackageAsync(new()
            {
                SellerID = request.SellerID,
                ColorMode = request.ColorMode,
                DuplexMode = request.DuplexMode,
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
