using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.ViewModels.Order
{
    public class VM_FilterGet_Order
    {
        public string FilterCustomerName { get; set; }
        public string FilterCustomerPhoneNumber { get; set; }
        public int FilterProductStatus { get; set; }
    }
}
