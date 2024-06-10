using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.FileDTOs;

namespace WebFotokopi.Application.Features.Queries.FileQueries.GetByIdFile
{
    public class GetByIdFileQueryHandler : IRequestHandler<GetByIdFileQueryRequest, GetByIdFileQueryResponse>
    {
        private readonly IFileService _fileService;

        public GetByIdFileQueryHandler(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<GetByIdFileQueryResponse> Handle(GetByIdFileQueryRequest request, CancellationToken cancellationToken)
        {
            GetByIdFileDTO getByIdFileDTO = await _fileService.GetByIdFileAsync(request.id);
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
