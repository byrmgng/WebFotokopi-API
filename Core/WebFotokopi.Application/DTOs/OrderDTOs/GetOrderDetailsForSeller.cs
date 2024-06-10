using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.DTOs.OrderDTOs
{
    public class GetOrderDetailsForSeller
    {
        public string OrderID { get; set; }
        public string CustomerName { get; set; }
        public string OrderStatus { get; set; }
        public ICollection<GetOrderDetailsItemForSeller> OrderItems { get; set; }
        public string Price { get; set; }
        public string CreatedDate { get; set; }
        public string DeliveryDate { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
    }
}
