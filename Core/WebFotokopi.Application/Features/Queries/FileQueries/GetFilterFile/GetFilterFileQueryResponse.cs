using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.FileDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.Features.Queries.FileQueries.GetFilterFile
{
    public class GetFilterFileQueryResponse
    {
        public IEnumerable<GetFileDTO> Files { get; set; }

    }
}
