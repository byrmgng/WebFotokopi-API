using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.CustomerAddressDTOs;
using WebFotokopi.Application.DTOs.FileDTOs;

namespace WebFotokopi.Application.Features.Commands.FileCommands.CreateFile
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommandRequest, CreateFileCommandResponse>
    {
        private readonly IFileService _fileService;

        public CreateFileCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<CreateFileCommandResponse> Handle(CreateFileCommandRequest request, CancellationToken cancellationToken)
        {
            CreateFileResponseDTO result = await _fileService.CreateFileAsync(new()
            {
                FileContent = request.FileContent,
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
