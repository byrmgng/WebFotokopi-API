using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.OrderRepositories;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.OrderRepositories
{
    public class OrderReadRepository : ReadRepositories<Order>, IOrderReadRepository
    {
        public OrderReadRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
