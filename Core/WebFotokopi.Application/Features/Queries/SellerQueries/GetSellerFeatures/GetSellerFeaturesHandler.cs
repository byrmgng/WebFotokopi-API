using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.SellerDTOs;

namespace WebFotokopi.Application.Features.Queries.SellerQueries.GetSellerFeatures
{
    public class GetSellerFeaturesHandler : IRequestHandler<GetSellerFeaturesRequest, GetSellerFeaturesResponse>
    {
        private readonly ISellerService _sellerService;

        public GetSellerFeaturesHandler(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public async Task<GetSellerFeaturesResponse> Handle(GetSellerFeaturesRequest request, CancellationToken cancellationToken)
        {
            ListSellerDTOs listSellerDTO = await _sellerService.SellerFeaturesAsync(request.SellerID);
            return new()
            {
                Seller = listSellerDTO,
            };
        }
    }
}
