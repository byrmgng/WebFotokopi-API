using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Domain.Entities;

namespace WebFotokopi.Application.Repositories.DistrictRepositories
{
    public interface IDistrictReadRepository:IReadRepository<District>
    {
        IQueryable<District> GetByCityIdAsync(int id, bool tracking = true);
    }
}
