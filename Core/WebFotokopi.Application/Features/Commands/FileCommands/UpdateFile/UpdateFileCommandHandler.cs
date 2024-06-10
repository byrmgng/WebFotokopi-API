using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.FileDTOs;

namespace WebFotokopi.Application.Features.Commands.FileCommands.UpdateFile
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommandRequest, UpdateFileCommandResponse>
    {
        private readonly IFileService _fileService;

        public UpdateFileCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<UpdateFileCommandResponse> Handle(UpdateFileCommandRequest request, CancellationToken cancellationToken)
        {
            UpdateFileResponseDTO result = await _fileService.UpdateFileAsync(new()
            {
                FileID = request.FileID,
                FileNote = request.FileNote,
                FileTitle = request.FileTitle,
            });
            return new()
            {
                Message = result.Message,
                Succeeded = result.Succeeded,
            };
        }
    }
}
