using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Queries.FileQueries.GetFilterFile
{
    public class GetFilterFileQueryRequest:IRequest<GetFilterFileQueryResponse>
    {
        public string FileTitle { get; set; }
        public string FileNote { get; set; }
    }
}
