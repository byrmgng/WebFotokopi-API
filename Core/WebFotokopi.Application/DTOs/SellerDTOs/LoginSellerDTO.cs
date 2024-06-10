using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.DTOs.SellerDTOs
{
    public class LoginSellerDTO:BaseResponseDTO
    {
        public TokenDTO Token { get; set; }
    }
}
