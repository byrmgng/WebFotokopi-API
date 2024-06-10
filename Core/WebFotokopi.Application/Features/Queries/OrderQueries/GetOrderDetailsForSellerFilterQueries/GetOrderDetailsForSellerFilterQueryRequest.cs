using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderDetailsForSellerFilterQueries
{
    public class GetOrderDetailsForSellerFilterQueryRequest:IRequest<GetOrderDetailsForSellerFilterQueryResponse>
    {
        public string FilterCustomerName { get; set; }
        public string FilterCustomerPhoneNumber { get; set; }
        public int FilterProductStatus { get; set; }
    }
}
