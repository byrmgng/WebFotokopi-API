using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Domain.Entities;

namespace WebFotokopi.Application.Repositories.ProductFeatureRepositories
{
    public interface IPackageReadReposity:IReadRepository<Package>
    {
        IQueryable<Package> GetBySellerIdAsync(string id, bool tracking = true);
    }
}
