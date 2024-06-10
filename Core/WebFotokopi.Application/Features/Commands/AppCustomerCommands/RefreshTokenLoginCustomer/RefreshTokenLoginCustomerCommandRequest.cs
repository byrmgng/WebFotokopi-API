using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.AppCustomerCommands.RefreshTokenLoginCustomer
{
    public class RefreshTokenLoginCustomerCommandRequest:IRequest<RefreshTokenLoginCustomerCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
