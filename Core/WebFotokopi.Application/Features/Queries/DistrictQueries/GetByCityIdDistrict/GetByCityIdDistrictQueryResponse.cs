using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.CityDTOs;
using WebFotokopi.Application.DTOs.DistrictDTOs;

namespace WebFotokopi.Application.Features.Queries.DistrictQueries.GetByIdDistrict
{
    public class GetByCityIdDistrictQueryResponse
    {
        public IEnumerable<ListDistrictDTO> Districts { get; set; }

    }
}
