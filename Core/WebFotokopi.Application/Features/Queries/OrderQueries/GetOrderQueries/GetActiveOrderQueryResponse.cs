using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.OrderDTOs;

namespace WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderQueries
{
    public class GetActiveOrderQueryRepsonse
    {
        public string SellerName { get; set; }
        public float Price { get; set; }
        public IEnumerable<GetOrderItem> Items { get; set; }
    }
}
