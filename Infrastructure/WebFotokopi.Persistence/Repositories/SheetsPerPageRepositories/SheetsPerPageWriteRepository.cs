using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.SellerSheetsPerPageRepositories;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.SellerSheetsPerPageRepositories
{
    public class SheetsPerPageWriteRepository : WriteRepository<SheetsPerPage>, ISheetsPerPageWriteReposity
    {
        public SheetsPerPageWriteRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
