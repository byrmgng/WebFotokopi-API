using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.DTOs.SellerDTOs;

namespace WebFotokopi.Application.Features.Queries.SellerQueries.FilterGetSeller
{
    public class FilterGetSellerQueryResponse
    {
        public IEnumerable<ListSellerDTOs> Sellers { get; set; }

    }
}
