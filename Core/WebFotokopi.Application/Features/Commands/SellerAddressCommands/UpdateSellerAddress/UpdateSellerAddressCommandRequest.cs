using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.SellerAddressCommands.UpdateSellerAddress
{
    public class UpdateSellerAddressCommandRequest:IRequest<UpdateSellerAddressCommandResponse>
    {
        public string DistrictID { get; set; }
        public string Address { get; set; }
    }
}
