using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.SellerDTOs;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.UpdateSellerLogo
{
    public class UpdateSellerLogoCommandHandler : IRequestHandler<UpdateSellerLogoCommandRequest, UpdateSellerLogoCommandResponse>
    {
        private readonly ISellerService _sellerService;
        public UpdateSellerLogoCommandHandler(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public async Task<UpdateSellerLogoCommandResponse> Handle(UpdateSellerLogoCommandRequest request, CancellationToken cancellationToken)
        {
            UpdateSellerDTO updateSellerDTO = await _sellerService.UpdeteSellerLogoAsync(new() { Logo = request.Logo });
            return new() { Message = updateSellerDTO.Message, Succeeded = updateSellerDTO.Succeeded };
        }
    }
}
