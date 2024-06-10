
using WebFotokopi.Application.DTOs.PackageDTOs;


namespace WebFotokopi.Application.Features.Queries.PackageQueries.GetAllPackage
{
    public class GetAllPackageQueryResponse
    {
        public IEnumerable<GetPackageDTO> Packages { get; set; }
    }
}
