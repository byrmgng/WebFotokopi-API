using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.DTOs
{
    public class BaseResponseDTO
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
