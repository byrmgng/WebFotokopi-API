using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.UpdateSellerLogo
{
    public class UpdateSellerLogoCommandRequest:IRequest<UpdateSellerLogoCommandResponse>
    {
        public IFormFile Logo { get; set; }

    }
}
