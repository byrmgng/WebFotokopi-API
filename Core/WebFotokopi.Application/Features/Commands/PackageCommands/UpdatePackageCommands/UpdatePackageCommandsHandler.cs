using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.Features.Commands.PackageCommands.UpdatePackageCommands
{
    public class UpdatePackageCommandsHandler : IRequestHandler<UpdatePackageCommandsRequest, UpdatePackageCommandsResponse>
    {
        private readonly IPackageService _packageService;

        public UpdatePackageCommandsHandler(IPackageService packageService)
        {
            _packageService = packageService;
        }
        public async Task<UpdatePackageCommandsResponse> Handle(UpdatePackageCommandsRequest request, CancellationToken cancellationToken)
        {
            UpdatePackageResponseDTO response = await _packageService.UpdatePackageAsync(new(){
                PackageId = request.PackageId,
                Name = request.PackageName,
                DuplexMode = request.DuplexMode,
                ColorMode = request.ColorMode,
                SheetsPerPageID = request.SheetsPerPageID,
                PaperSizeID = request.PaperSizeID,
                PaperTypeID = request.PaperTypeID,
                isActive=request.isActive,
                Price = request.Price
            });
            return new()
            {
                Succeeded = response.Succeeded,
                Message = response.Message,
            };
        }
    }
}
