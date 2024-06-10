using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.DTOs.CustomerDTOs
{
    public class LoginCustomerDTO:BaseResponseDTO
    {
        public TokenDTO Token { get; set; }
    }
}
