using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.DTOs.FileDTOs
{
    public class GetFileDTO
    {
        public string ID { get; set; }
        public string FileTitle { get; set; }
        public string FileNote { get; set; }
        public int NumberOfPage { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }  
}
