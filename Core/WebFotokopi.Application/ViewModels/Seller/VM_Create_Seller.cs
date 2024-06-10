using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.ViewModels.Seller
{
    public class VM_Create_Seller
    {
        public string CompanyName { get; set; }
        public int DistrictID { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
    }
}
