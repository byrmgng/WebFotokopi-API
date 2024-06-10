using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.FileRepositories;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.FileRepositories
{
    public class FileReadRepository : ReadRepositories<WebFotokopi.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
