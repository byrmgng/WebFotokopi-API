using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.LoginSeller
{
    public class LoginSellerCommandResponse
    {
        public TokenDTO Token { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
