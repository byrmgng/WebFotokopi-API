using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.CityRepositories;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.CityRepositories
{
    public class CityWriteRepository : WriteRepository<City>, ICityWriteRepository
    {
        public CityWriteRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
