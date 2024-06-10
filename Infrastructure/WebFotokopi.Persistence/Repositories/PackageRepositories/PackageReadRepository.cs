using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.ProductFeatureRepositories;
using WebFotokopi.Application.Repositories.ProductRepositories;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.ProductFeatureRepositories
{
    public class PackageReadRepository : ReadRepositories<Package>, IPackageReadReposity
    {
        public PackageReadRepository(WebFotokopiDbContext context) : base(context)
        {
        }

        public IQueryable<Package> GetBySellerIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query.Where(package => package.SellerID == id);
        }
    }
}
