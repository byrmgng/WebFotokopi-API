using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.OrderCommands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandRequest:IRequest<UpdateOrderStatusCommandResponse>
    {
        public string OrderID { get; set; }
        public string Status { get; set; }
    }
}
