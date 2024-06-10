using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.ProductCommands.DeleteProductCommands
{
    public class DeleteProductCommandRequest:IRequest<DeleteProductCommandResponse>
    {
        public string ProductID { get; set; }
    }
}
