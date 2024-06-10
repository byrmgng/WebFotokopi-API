using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.Features.Commands.PackageCommands.DeletePackageCommands
{
    public class DeletePackageCommandsHandler : IRequestHandler<DeletePackageCommandsRequest, DeletePackageCommandsResponse>
    {
        private readonly IPackageService _packageService;

        public DeletePackageCommandsHandler(IPackageService packageService)
        {
            _packageService = packageService;
        }
        public async Task<DeletePackageCommandsResponse> Handle(DeletePackageCommandsRequest request, CancellationToken cancellationToken)
        {
            DeletePackageResponseDTO response = await _packageService.DeletePackageAsync(request.PackageID);
            return new()
            {
                Succeeded = response.Succeeded,
                Message = response.Message,
            };
        }
    }
}
