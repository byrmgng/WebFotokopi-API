using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Domain.Entities.Commons;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Domain.Entities
{
    public class Order:BaseEntity
    {
        public ICollection<Product> Products { get; set; }
        public int Status { get; set; }
        public string CustomerID { get; set; }
        public AppCustomer AppCustomer { get; set; }
        public float Price { get; set; }
        public AppSeller AppSeller { get; set; }
        public string? SellerID { get; set; }
    }
}
