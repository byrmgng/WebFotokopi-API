using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.OrderDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderDetailsQueries
{
    public class GetOrderDetailsQueryResponse
    {
        public IEnumerable<GetOrderDetails> OrderDetails { get; set; }

    }
}
