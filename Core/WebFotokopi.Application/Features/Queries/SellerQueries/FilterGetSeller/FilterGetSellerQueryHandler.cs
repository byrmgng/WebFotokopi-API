using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.DTOs.SellerDTOs;

namespace WebFotokopi.Application.Features.Queries.SellerQueries.FilterGetSeller
{
    public class FilterGetSellerQueryHandler : IRequestHandler<FilterGetSellerQueryRequest, FilterGetSellerQueryResponse>
    {
        private readonly ISellerService _sellerService;

        public FilterGetSellerQueryHandler(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public async Task<FilterGetSellerQueryResponse> Handle(FilterGetSellerQueryRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<ListSellerDTOs> listSellerDTOs = await _sellerService.ListSellerAsync(new()
            {
                SellerName = request.SellerName,
                SheetsPerPageID = request.SheetsPerPageID,
                PaperTypeID = request.PaperTypeID,
                PaperSizeID = request.PaperSizeID,
                ColorMode = request.ColorMode,
                DuplexMode = request.DuplexMode,
            });
            return new()
            {
                Sellers = listSellerDTOs,
            };
        }
    }
}
