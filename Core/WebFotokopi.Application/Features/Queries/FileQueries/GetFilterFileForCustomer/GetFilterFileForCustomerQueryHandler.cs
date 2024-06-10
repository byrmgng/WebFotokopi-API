using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.FileDTOs;

namespace WebFotokopi.Application.Features.Queries.FileQueries.GetFilterFileForCustomer
{
    public class GetFilterFileForCustomerQueryHandler:IRequestHandler<GetFilterFileForCustomerQueryRequest,GetFilterFileForCustomerQueryResponse>
    {
        private readonly IFileService _fileService;

        public GetFilterFileForCustomerQueryHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<GetFilterFileForCustomerQueryResponse> Handle(GetFilterFileForCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<GetFileDTO> getFileDTOs = await _fileService.GetFileForCustomerAsync(new()
            {
                SellerID = request.SellerID,
                FileNote = request.FileNote,
                FileTitle = request.FileTitle,
            });
            return new()
            {
                Files = getFileDTOs,
            };
        }
    }
}
