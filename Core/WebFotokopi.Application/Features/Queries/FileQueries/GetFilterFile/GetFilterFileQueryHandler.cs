using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.FileDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.Features.Queries.FileQueries.GetFilterFile
{
    public class GetFilterFileQueryHandler : IRequestHandler<GetFilterFileQueryRequest, GetFilterFileQueryResponse>
    {
        private readonly IFileService _fileService;

        public GetFilterFileQueryHandler(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<GetFilterFileQueryResponse> Handle(GetFilterFileQueryRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<GetFileDTO> getFileDTOs = await _fileService.GetFileAsync(new()
            {
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
