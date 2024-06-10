using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.FileRepositories;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.FileRepositories
{
    public class FileWriteRepository : WriteRepository<WebFotokopi.Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
