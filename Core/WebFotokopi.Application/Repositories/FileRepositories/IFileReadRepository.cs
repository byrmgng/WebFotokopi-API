﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Domain.Entities;

namespace WebFotokopi.Application.Repositories.FileRepositories
{
    public interface IFileReadRepository: IReadRepository<WebFotokopi.Domain.Entities.File>
    {
    }
}
