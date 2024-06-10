using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.ProductRepositories;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.ProductRepositories
{
    public class ProductReadRepository : ReadRepositories<Product>, IProductReadRepository
    {
        public ProductReadRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
