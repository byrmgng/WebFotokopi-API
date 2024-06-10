using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.FileCommands.CreateFile
{
    public class CreateFileCommandRequest:IRequest<CreateFileCommandResponse>
    {
        public  string FileTitle { get; set; }
        public  string FileNote { get; set; }
        public  IFormFile FileContent { get; set; }
    }
}
