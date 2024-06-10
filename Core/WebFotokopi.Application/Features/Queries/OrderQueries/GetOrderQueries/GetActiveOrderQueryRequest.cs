using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Queries.OrderQueries.GetOrderQueries
{
    public class GetActiveOrderQueryRequest:IRequest<GetActiveOrderQueryRepsonse>
    {
    }
}
