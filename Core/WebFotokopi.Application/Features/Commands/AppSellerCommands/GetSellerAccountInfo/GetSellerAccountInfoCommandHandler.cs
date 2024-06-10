using MediatR;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.SellerDTOs;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.GetSellerAccountInfo
{
    public class GetSellerAccountInfoCommandHandler : IRequestHandler<GetSellerAccountInfoCommandRequest, GetSellerAccountInfoCommandResponse>
    {
        private readonly ISellerService _sellerService;


        public GetSellerAccountInfoCommandHandler(ISellerService sellerService)
        {
            _sellerService = sellerService;

        }
        public async Task<GetSellerAccountInfoCommandResponse> Handle(GetSellerAccountInfoCommandRequest request, CancellationToken cancellationToken)
        {
            GetSellerAccountInfoDTO dto = await _sellerService.GetSellerAccountInfoAsync();
            return new()
            {
                GetSellerAccountInfo = dto,
            };

        }
    }
}
