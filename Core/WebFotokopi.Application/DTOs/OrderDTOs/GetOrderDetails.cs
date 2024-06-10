using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.DTOs.OrderDTOs
{
    public class GetOrderDetails
    {
        public string OrderID { get; set; }
        public string CompanyName { get; set; }
        public string OrderStatus { get; set; }
        public ICollection<GetOrderDetailsItem> OrderItems { get; set; }
        public string Price { get; set; }
        public string CreatedDate { get; set; }
        public string DeliveryDate { get; set; }
    }
}
