using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.ProductFeatureRepositories;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.ProductFeatureRepositories
{
    public class PackageWriteRepository : WriteRepository<Package>, IPackageWriteRepository
    {
        public PackageWriteRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
