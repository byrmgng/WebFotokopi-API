using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.CreateSeller
{
    public class CreateSellerCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
