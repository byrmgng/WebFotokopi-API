using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.ProductCommands.CreateProductForCustomerCommands
{
    public class CreateProductForCustomerCommandRequest:IRequest<CreateProductForCustomerCommandResponse>
    {
        public string FileTitle { get; set; }
        public string FileNote { get; set; }
        public IFormFile FileContent { get; set; }
        public string AppSellerID { get; set; }
        public string PackageID { get; set; }
        public string CustomerNote { get; set; }
        public int Quantity { get; set; }
    }
}
