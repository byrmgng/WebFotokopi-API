using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WebFotokopiDbContext>
    {
        public WebFotokopiDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<WebFotokopiDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(Configurations.ConnectionString);
            return new WebFotokopiDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
