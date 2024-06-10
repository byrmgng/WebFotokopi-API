using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.FileCommands.UpdateFile
{
    public class UpdateFileCommandRequest:IRequest<UpdateFileCommandResponse>
    {
        public string FileTitle { get; set; }
        public string FileNote { get; set; }
        public string FileID { get; set; }
    }
}
