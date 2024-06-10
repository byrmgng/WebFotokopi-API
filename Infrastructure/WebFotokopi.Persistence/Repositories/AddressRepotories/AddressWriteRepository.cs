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
    public class AddressWriteRepository : WriteRepository<SellerAddress>, ISellerAddressWriteRepository
    {
        public AddressWriteRepository(WebFotokopiDbContext context) : base(context)
        {

        }
        
    }
}
