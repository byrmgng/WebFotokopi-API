using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.CustomerDTOs;
using WebFotokopi.Application.DTOs.SellerDTOs;

namespace WebFotokopi.Application.Features.Commands.AppCustomerCommands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommandRequest, CreateCustomerCommandResponse>
    {
        private readonly ICustomerService _customerService;


        public CreateCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;

        }
        public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            CreateCustomerDTO result = await _customerService.CreateCustomerAsync(new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Password = request.Password,
                PasswordAgain = request.PasswordAgain,
                DistrictID = request.DistrictID,
                Address = request.Address
            });
            return new()
            {
                Succeeded = result.Succeeded,
                Message = result.Message,
            };
        }
    }
}
