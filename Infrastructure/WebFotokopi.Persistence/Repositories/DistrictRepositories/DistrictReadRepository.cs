using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories;
using WebFotokopi.Application.Repositories.DistrictRepositories;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.DistrictRepositories
{
    public class DistrictReadRepository : ReadRepositories<District>, IDistrictReadRepository
    {
        public DistrictReadRepository(WebFotokopiDbContext context) : base(context)
        {
            
        }
        public IQueryable<District> GetByCityIdAsync(int id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query.Where(city => city.CityID == id);
        }
    }
}
