using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.FileDTOs;

namespace WebFotokopi.Application.Features.Queries.FileQueries.GetFilterFileForCustomer
{
    public class GetFilterFileForCustomerQueryResponse
    {
        public IEnumerable<GetFileDTO> Files { get; set; }

    }
}
