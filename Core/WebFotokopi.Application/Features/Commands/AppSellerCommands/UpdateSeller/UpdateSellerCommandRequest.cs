using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.UpdateSeller
{
    public class UpdateSellerCommandRequest:IRequest<UpdateSellerCommandResponse>
    {
        public string CompanyName { get; set; }
        public string Description { get; set; }
    }
}
