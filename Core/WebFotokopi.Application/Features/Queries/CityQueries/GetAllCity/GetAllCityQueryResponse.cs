using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.CityDTOs;
using WebFotokopi.Domain.Entities;

namespace WebFotokopi.Application.Features.Queries.CityQueries.GetAllCity
{
    public class GetAllCityQueryResponse
    {
        public IEnumerable<ListCityDTO> Cities { get; set; }
    }
}
