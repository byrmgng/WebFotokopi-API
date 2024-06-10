using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Commands.FileCommands.DeleteFile
{
    public class DeleteFileCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
