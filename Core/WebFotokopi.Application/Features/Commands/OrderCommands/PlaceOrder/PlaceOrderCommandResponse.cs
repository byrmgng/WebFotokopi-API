using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.OrderCommands.PlaceOrder
{
    public class PlaceOrderCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
