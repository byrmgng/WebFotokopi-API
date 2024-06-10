using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.RefleshTokenLoginSeller
{
    public class RefreshTokenLoginSellerCommandRequest:IRequest<RefreshTokenLoginSellerCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
