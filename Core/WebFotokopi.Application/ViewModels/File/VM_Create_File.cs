using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.ViewModels.File
{
    public class VM_Create_File
    {
        public required string FileTitle { get; set; }
        public required string FileNote { get; set; }
        public required IFormFile FileContent { get; set; }

    }
}
