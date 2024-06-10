using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.SellerAddressCommands.CreateSellerAddress
{
    public class CreateSellerAddressCommandRequest:IRequest<CreateSellerAddressCommandResponse>
    {
        public int DistrictID { get; set; }
        public string Address { get; set; }
    }
}
