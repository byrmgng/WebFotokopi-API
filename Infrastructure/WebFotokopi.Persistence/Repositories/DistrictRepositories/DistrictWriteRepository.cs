using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.DistrictRepositories;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.DistrictRepositories
{
    public class DistrictWriteRepository : WriteRepository<District>, IDistrictWriteRepository
    {
        public DistrictWriteRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
