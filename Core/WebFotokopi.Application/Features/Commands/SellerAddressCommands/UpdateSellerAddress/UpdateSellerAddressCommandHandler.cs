using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.SellerAddressDTOs;

namespace WebFotokopi.Application.Features.Commands.SellerAddressCommands.UpdateSellerAddress
{
    public class UpdateSellerAddressCommandHandler : IRequestHandler<UpdateSellerAddressCommandRequest, UpdateSellerAddressCommandResponse>
    {
        private readonly ISellerAddressService _sellerAddressService;

        public UpdateSellerAddressCommandHandler(ISellerAddressService sellerAddressService)
        {
            _sellerAddressService = sellerAddressService;
        }
        public async Task<UpdateSellerAddressCommandResponse> Handle(UpdateSellerAddressCommandRequest request, CancellationToken cancellationToken)
        {
            UpdateSellerAddressDTO dto = await _sellerAddressService.UpdateSellerAddress(new() { Address = request.Address,DistrictID=Convert.ToInt32(request.DistrictID) });
            return new() { Message = dto.Message,Succeeded = dto.Succeeded };
        }
    }
}
