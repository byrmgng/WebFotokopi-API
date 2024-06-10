using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Domain.Entities;

namespace WebFotokopi.Application.Features.Commands.PackageCommands.AddPackageCommands
{
    public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommandRequest, CreatePackageCommandResponse>
    {
        private readonly IPackageService _packageService;

        public CreatePackageCommandHandler(IPackageService packageService)
        {
            _packageService = packageService;
        }
        public async Task<CreatePackageCommandResponse> Handle(CreatePackageCommandRequest request, CancellationToken cancellationToken)
        {
            CreatePackageResponseDTO response = await _packageService.CreatePackageAsync(new()
            {
                Name = request.PackageName,
                Price = request.Price,
                ColorMode = request.ColorMode,
                DuplexMode = request.DuplexMode,
                SheetsPerPageID = request.SheetsPerPageID,
                PaperTypeID = request.PaperTypeID,
                PaperSizeID =request.PaperSizeID,
                isActive = request.isActive
            });
            return (new() { Message = response.Message,Succeeded=response.Succeeded});
        }
    }
}
