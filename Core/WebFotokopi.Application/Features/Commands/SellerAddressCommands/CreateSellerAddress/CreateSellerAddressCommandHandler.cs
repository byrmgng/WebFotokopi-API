using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.SellerAddressDTOs;

namespace WebFotokopi.Application.Features.Commands.SellerAddressCommands.CreateSellerAddress
{
    public class CreateSellerAddressCommandHandler : IRequestHandler<CreateSellerAddressCommandRequest, CreateSellerAddressCommandResponse>
    {
        private readonly ISellerAddressService _sellerAddressService;

        public CreateSellerAddressCommandHandler(ISellerAddressService sellerAddressService)
        {
            _sellerAddressService = sellerAddressService;
        }
        public async Task<CreateSellerAddressCommandResponse> Handle(CreateSellerAddressCommandRequest request, CancellationToken cancellationToken)
        {
            CreateSellerAddressDTO result = await _sellerAddressService.CreateSellerAddressAsync(new()
            {
                Address = request.Address,
                DistrictID = request.DistrictID,
            });
            return new()
            {
                AddressID = result.Id,
                Succeeded = result.Succeeded,
                Message = result.Message
            };
        }
    }
}
