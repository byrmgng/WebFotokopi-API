using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Queries.DistrictQueries.GetByIdDistrict
{
    public class GetByCityIdDistrictQueryRequest:IRequest<GetByCityIdDistrictQueryResponse>
    {
        public int CityID { get; set; }
    }
}
