using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.ProductCommands.CreateProductCommands
{
    public class CreateProductCommandRequest:IRequest<CreateProductCommandResponse>
    {
        public string FileID { get; set; }
        public string PackageID { get; set; }
        public string CustomerNote { get; set; }
        public int Quantity { get; set; }
    }
}
