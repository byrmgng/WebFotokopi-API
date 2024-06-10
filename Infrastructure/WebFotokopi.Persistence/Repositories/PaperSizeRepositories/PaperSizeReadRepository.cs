using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.SellerPaperSizeRepositories;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.SellerPaperSizeRepositories
{
    public class PaperSizeReadRepository : ReadRepositories<PaperSize>, IPaperSizeReadRepository
    {
        public PaperSizeReadRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
