using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.SellerDTOs;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.LoginSeller
{
    public class LoginSellerCommandHandler : IRequestHandler<LoginSellerCommandRequest, LoginSellerCommandResponse>
    {
        private readonly ISellerService _sellerService;


        public LoginSellerCommandHandler(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public async Task<LoginSellerCommandResponse> Handle(LoginSellerCommandRequest request, CancellationToken cancellationToken)
        {
            LoginSellerDTO result = await _sellerService.LoginSellerAsync(new(){
                MailorPhoneNumber = request.MailorPhoneNumber,
                Password= request.Password,
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
