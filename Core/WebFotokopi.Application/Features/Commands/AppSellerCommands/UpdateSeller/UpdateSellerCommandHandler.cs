using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.SellerDTOs;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.UpdateSeller
{
    public class UpdateSellerCommandHandler : IRequestHandler<UpdateSellerCommandRequest, UpdateSellerCommandResponse>
    {
        private readonly ISellerService _sellerService;
        public UpdateSellerCommandHandler(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public async Task<UpdateSellerCommandResponse> Handle(UpdateSellerCommandRequest request, CancellationToken cancellationToken)
        {
            UpdateSellerDTO updateSellerDTO = await _sellerService.UpdateSellerAsync(new() { CompanyName=request.CompanyName,Description=request.Description});
            return new() { Message = updateSellerDTO.Message,Succeeded = updateSellerDTO.Succeeded};
        }
    }
}
