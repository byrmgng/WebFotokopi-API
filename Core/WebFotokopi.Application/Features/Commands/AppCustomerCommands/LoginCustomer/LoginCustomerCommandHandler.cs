using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.CustomerDTOs;
using WebFotokopi.Application.DTOs.SellerDTOs;

namespace WebFotokopi.Application.Features.Commands.AppCustomerCommands.LoginCustomer
{
    public class LoginCustomerCommandHandler:IRequestHandler<LoginCustomerCommandRequest, LoginCustomerCommandResponse>
    {
        private readonly ICustomerService _customerService;


        public LoginCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<LoginCustomerCommandResponse> Handle(LoginCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            LoginCustomerDTO result = await _customerService.LoginCustomerAsync(new()
            {
                MailorPhoneNumber = request.MailorPhoneNumber,
                Password = request.Password,
            });
            return new()
            {
                Succeeded = result.Succeeded,
                Message = result.Message,
                Token = result.Token,
            };
        }
    }
}
