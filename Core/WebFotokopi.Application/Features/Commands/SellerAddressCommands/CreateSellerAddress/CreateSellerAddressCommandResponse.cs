using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.SellerAddressCommands.CreateSellerAddress
{
    public class CreateSellerAddressCommandResponse
    {
        public bool Succeeded { get; set; }
        public Guid AddressID { get; set; }
        public string Message { get; set; }
    }
}
