using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.CityDTOs;
using WebFotokopi.Application.Repositories.CityRepositories;
using WebFotokopi.Domain.Entities;

namespace WebFotokopi.Persistence.Services
{
    public class CityService : ICityService
    {
        readonly ICityReadRepository _cityReadRepository;
        public CityService(ICityReadRepository cityReadRepository)
        {
            _cityReadRepository = cityReadRepository;
        }
        public async Task<IEnumerable<ListCityDTO>> GetAllCityAsync()
        {
            List<City> cities = await _cityReadRepository.GetAll(false).ToListAsync();
            return cities.Select(x => new ListCityDTO
            {
                CityID = x.ID,
                CityName = x.Name
            });
        }
    }
}
