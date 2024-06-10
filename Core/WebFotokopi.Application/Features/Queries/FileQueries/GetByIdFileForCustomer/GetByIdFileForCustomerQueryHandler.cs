using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.FileDTOs;

namespace WebFotokopi.Application.Features.Queries.FileQueries.GetByIdFileForCustomer
{
    public class GetByIdFileForCustomerQueryHandler : IRequestHandler<GetByIdFileForCustomerQueryRequest, GetByIdFileForCustomerQueryResponse>
    {
        private readonly IFileService _fileService;
        private readonly IOrderService _orderService;

        public GetByIdFileForCustomerQueryHandler(IFileService fileService, IOrderService orderService)
        {
            _fileService = fileService;
            _orderService = orderService;
        }
        public async Task<GetByIdFileForCustomerQueryResponse> Handle(GetByIdFileForCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            GetByIdFileDTO getByIdFileDTO = await _fileService.GetByIdFileForCustomerAsync(request.id);
            return new()
            {
                ContentType = getByIdFileDTO.ContentType,
                FileContent = getByIdFileDTO.FileContent,
                FileName = getByIdFileDTO.FileName,
                FileID = getByIdFileDTO.FileID,
                FileNote = getByIdFileDTO.FileNote,
                FileTitle = getByIdFileDTO.FileTitle,
            };
        }
    }
}
