using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.SellerAddressRepositories;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.SellerAddressRepotories
{
    public class AddressReadRepository : ReadRepositories<SellerAddress>, ISellerAddressReadRepository
    {
        public AddressReadRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
