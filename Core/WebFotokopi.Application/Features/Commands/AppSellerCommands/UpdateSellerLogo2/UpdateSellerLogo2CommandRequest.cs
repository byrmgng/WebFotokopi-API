using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.UpdateSellerLogo2
{
    public class UpdateSellerLogo2CommandRequest:IRequest<UpdateSellerLogo2CommandResponse>
    {
        public IFormFile Logo2 { get; set; }

    }
}
