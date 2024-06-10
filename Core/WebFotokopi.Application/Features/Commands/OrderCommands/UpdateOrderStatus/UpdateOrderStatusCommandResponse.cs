using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.OrderCommands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
