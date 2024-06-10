using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.CityDTOs;
using WebFotokopi.Application.DTOs.DistrictDTOs;
using WebFotokopi.Application.Repositories.DistrictRepositories;
using WebFotokopi.Domain.Entities;

namespace WebFotokopi.Persistence.Services
{
    public class DistrictService : IDistrictService
    {
        readonly IDistrictReadRepository _districtReadRepository;
        public DistrictService(IDistrictReadRepository districtReadRepository)
        {
            _districtReadRepository = districtReadRepository;
        }
        public async Task<IEnumerable<ListDistrictDTO>> GetByCityIdDistrictAsync(int id)
        {
            List<District> districts = await _districtReadRepository.GetByCityIdAsync(id, false).ToListAsync();
            return districts.Select(x => new ListDistrictDTO
            {
                DistrictID = x.ID,
                DistrictName = x.Name
            });
        }
    }
}
