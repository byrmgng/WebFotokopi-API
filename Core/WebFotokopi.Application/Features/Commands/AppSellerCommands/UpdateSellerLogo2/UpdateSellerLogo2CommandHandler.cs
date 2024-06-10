using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.SellerDTOs;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.UpdateSellerLogo2
{
    public class UpdateSellerLogo2CommandHandler : IRequestHandler<UpdateSellerLogo2CommandRequest, UpdateSellerLogo2CommandResponse>
    {
        private readonly ISellerService _sellerService;
        public UpdateSellerLogo2CommandHandler(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public async Task<UpdateSellerLogo2CommandResponse> Handle(UpdateSellerLogo2CommandRequest request, CancellationToken cancellationToken)
        {
            UpdateSellerDTO updateSellerDTO = await _sellerService.UpdeteSellerLogo2Async(new() { Logo = request.Logo2 });
            return new() { Message = updateSellerDTO.Message, Succeeded = updateSellerDTO.Succeeded };
        }
    }
}
