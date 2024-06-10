using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.AppCustomerCommands.LoginCustomer
{
    public class LoginCustomerCommandRequest:IRequest<LoginCustomerCommandResponse>
    {
        public string MailorPhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
