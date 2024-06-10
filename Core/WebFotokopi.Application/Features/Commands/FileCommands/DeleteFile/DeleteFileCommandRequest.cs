using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.FileCommands.DeleteFile
{
    public class DeleteFileCommandRequest:IRequest<DeleteFileCommandResponse>
    {
        public string FileID { get; set; }

    }
}
