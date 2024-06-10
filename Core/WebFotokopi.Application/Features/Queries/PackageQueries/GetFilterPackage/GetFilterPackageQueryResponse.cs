using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.Features.Queries.PackageQueries.FilterGetPackage
{
    public class GetFilterPackageQueryResponse
    {
        public IEnumerable<GetPackageDTO> Packages { get; set; }

    }
}
