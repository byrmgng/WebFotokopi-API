using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.PackageCommands.DeletePackageCommands
{
    public class DeletePackageCommandsRequest:IRequest<DeletePackageCommandsResponse>
    {
        public string PackageID { get; set; }
    }
}
