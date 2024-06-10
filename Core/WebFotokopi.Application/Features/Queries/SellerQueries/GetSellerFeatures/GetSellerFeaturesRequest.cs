using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Queries.SellerQueries.GetSellerFeatures
{
    public class GetSellerFeaturesRequest:IRequest<GetSellerFeaturesResponse>
    {
        public string SellerID { get; set; }
    }
}
