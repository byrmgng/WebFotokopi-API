using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.CityDTOs;

namespace WebFotokopi.Application.Abstraction.Services
{
    public interface ICityService
    {
        Task<IEnumerable<ListCityDTO>> GetAllCityAsync();
    }
}
