﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.SellerPaperTypeRepositories;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.SellerPaperTypeRepositories
{
    public class PaperTypeReadRepository : ReadRepositories<PaperType>, IPaperTypeReadRepository
    {
        public PaperTypeReadRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
