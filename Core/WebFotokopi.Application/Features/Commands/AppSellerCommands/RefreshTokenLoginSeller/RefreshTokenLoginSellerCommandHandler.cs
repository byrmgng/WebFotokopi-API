using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.SellerDTOs;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.RefleshTokenLoginSeller
{
    public class RefreshTokenLoginSellerCommandHandler : IRequestHandler<RefreshTokenLoginSellerCommandRequest, RefreshTokenLoginSellerCommandResponse>
    {
        private readonly ISellerService _sellerService;

        public RefreshTokenLoginSellerCommandHandler(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public async Task<RefreshTokenLoginSellerCommandResponse> Handle(RefreshTokenLoginSellerCommandRequest request, CancellationToken cancellationToken)
        {
            LoginSellerDTO result = await _sellerService.RefreshTokenLoginSellerAsync(request.RefreshToken);
            return new()
            {
                Succeeded = result.Succeeded,
                Token = result.Token,
            };
        }
    }
}
