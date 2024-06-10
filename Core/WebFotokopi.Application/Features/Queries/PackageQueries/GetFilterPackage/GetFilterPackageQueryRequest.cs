using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Queries.PackageQueries.FilterGetPackage
{
    public class GetFilterPackageQueryRequest:IRequest<GetFilterPackageQueryResponse>
    {
        public string PackageName { get; set; }
        public string PaperSizeID { get; set; }
        public string PaperTypeID { get; set; }
        public string SheetsPerPageID { get; set; }
        public string DuplexMode { get; set; }
        public string ColorMode { get; set; }
        public string IsActive { get; set; }
    }
}
