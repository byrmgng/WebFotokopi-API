using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.DTOs.OrderDTOs
{
    public class GetOrderItems
    {
        public string SellerName { get; set; }
        public float Price { get; set; }
        public IEnumerable<GetOrderItem> Items { get; set; }
    }
}
