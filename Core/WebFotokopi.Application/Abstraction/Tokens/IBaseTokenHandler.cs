using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Application.Abstraction.Tokens
{
    public interface IBaseTokenHandler
    {

         string CreateRefreshToken();    
    }
}
