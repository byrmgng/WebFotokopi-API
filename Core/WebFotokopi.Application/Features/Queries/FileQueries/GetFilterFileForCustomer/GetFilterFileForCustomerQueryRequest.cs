using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Queries.FileQueries.GetFilterFileForCustomer
{
    public class GetFilterFileForCustomerQueryRequest:IRequest<GetFilterFileForCustomerQueryResponse>
    {
        public string SellerID { get; set; }
        public string FileTitle { get; set; }
        public string FileNote { get; set; }
    }
}
