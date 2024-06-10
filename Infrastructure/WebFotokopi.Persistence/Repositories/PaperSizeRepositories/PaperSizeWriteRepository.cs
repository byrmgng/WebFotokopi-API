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
    public class PaperSizeWriteRepository : WriteRepository<PaperSize>, IPaperSizeWriteRepository
    {
        public PaperSizeWriteRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
