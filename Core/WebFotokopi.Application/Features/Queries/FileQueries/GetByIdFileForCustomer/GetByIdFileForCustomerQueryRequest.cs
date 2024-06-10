using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Queries.FileQueries.GetByIdFileForCustomer
{
    public class GetByIdFileForCustomerQueryRequest:IRequest<GetByIdFileForCustomerQueryResponse>
    {
        public string id { get; set; }

    }
}
