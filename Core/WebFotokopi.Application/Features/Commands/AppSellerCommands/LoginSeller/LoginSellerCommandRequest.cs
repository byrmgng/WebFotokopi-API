using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.LoginSeller
{
    public class LoginSellerCommandRequest:IRequest<LoginSellerCommandResponse>
    {
        public string MailorPhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
