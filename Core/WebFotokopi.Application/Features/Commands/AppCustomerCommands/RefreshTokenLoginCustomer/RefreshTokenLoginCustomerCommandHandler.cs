using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.CustomerDTOs;
using WebFotokopi.Application.DTOs.SellerDTOs;

namespace WebFotokopi.Application.Features.Commands.AppCustomerCommands.RefreshTokenLoginCustomer
{
    public class RefreshTokenLoginCustomerCommandHandler:IRequestHandler<RefreshTokenLoginCustomerCommandRequest,RefreshTokenLoginCustomerCommandResponse>
    {

        private readonly ICustomerService _customerService;

        public RefreshTokenLoginCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<RefreshTokenLoginCustomerCommandResponse> Handle(RefreshTokenLoginCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            LoginCustomerDTO result = await _customerService.RefreshTokenLoginCustomerAsync(request.RefreshToken);
            return new()
            {
                Succeeded = result.Succeeded,
                Token = result.Token,
            };
        }
    }
}
