using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.OrderDTOs;

namespace WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderDetailsForSellerFilterQueries
{
    public class GetOrderDetailsForSellerFilterQueryResponse
    {
        public IEnumerable<GetOrderDetailsForSeller> OrderDetails { get; set; }

    }
}
