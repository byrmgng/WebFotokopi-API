using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.FileDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.Features.Commands.FileCommands.DeleteFile
{
    public class DeleteFileCommandHandler:IRequestHandler<DeleteFileCommandRequest, DeleteFileCommandResponse> 
    {
        private readonly IFileService _fileService;

        public DeleteFileCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<DeleteFileCommandResponse> Handle(DeleteFileCommandRequest request, CancellationToken cancellationToken)
        {
            DeleteFileResponseDTO response = await _fileService.DeleteFileAsync(request.FileID);
            return new()
            {
                Succeeded = response.Succeeded,
                Message = response.Message,
            };
        }
    }
}
