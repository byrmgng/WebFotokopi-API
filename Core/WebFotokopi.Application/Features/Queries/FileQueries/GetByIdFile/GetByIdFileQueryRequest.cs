using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Queries.FileQueries.GetByIdFile
{
    public class GetByIdFileQueryRequest:IRequest<GetByIdFileQueryResponse>
    {
        public string id { get; set; }
    }
}
