using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Domain.Entities.Identity
{
    public class AppCustomer: BaseIdentityUser
    {
        public CustomerAddress CustomerAddress { get; set; }
        public Guid CustomerAddressID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
