using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.DTOs.OrderDTOs
{
    public class GetOrderItem
    {
        public Guid ProductID { get; set; }
        public string FileTitle { get; set; }
        public int Quantity { get; set; }
        public int NumberOfPage { get; set; }
        public float Price { get; set; }
        public float TotalPrice { get; set; }
    }
}
