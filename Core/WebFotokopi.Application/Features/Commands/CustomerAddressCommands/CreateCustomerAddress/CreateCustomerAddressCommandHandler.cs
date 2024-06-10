using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.CustomerAddressDTOs;
using WebFotokopi.Application.DTOs.SellerAddressDTOs;
using WebFotokopi.Application.Features.Commands.SellerAddressCommands.CreateSellerAddress;

namespace WebFotokopi.Application.Features.Commands.CustomerAddressCommands.CreateCustomerAddress
{
    public class CreateCustomerAddressCommandHandler:IRequestHandler<CreateCustomerAddressCommandRequest,CreateCustomerAddressCommandResponse>
    {
        private readonly ICustomerAddressService _customerAddressService;

        public CreateCustomerAddressCommandHandler(ICustomerAddressService customerAddressService)
        {
            _customerAddressService = customerAddressService;
        }
        public async Task<CreateCustomerAddressCommandResponse> Handle(CreateCustomerAddressCommandRequest request, CancellationToken cancellationToken)
        {
            CreateCustomerAddressDTO result = await _customerAddressService.CreateCustomerAddressAsync(new()
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
