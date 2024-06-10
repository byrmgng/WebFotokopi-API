using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.ViewModels.Seller
{
    public class VM_Update_SellerLogo
    {
        public required IFormFile Logo { get; set; }

    }
}
