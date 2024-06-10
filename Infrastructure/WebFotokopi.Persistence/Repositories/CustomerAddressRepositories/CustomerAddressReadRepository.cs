﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Repositories.CustomerAddressRepositories;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Persistence.Contexts;

namespace WebFotokopi.Persistence.Repositories.CustomerAddressRepositories
{
    public class CustomerAddressReadRepository : ReadRepositories<CustomerAddress>, ICustomerAddressReadRepository
    {
        public CustomerAddressReadRepository(WebFotokopiDbContext context) : base(context)
        {
        }
    }
}
