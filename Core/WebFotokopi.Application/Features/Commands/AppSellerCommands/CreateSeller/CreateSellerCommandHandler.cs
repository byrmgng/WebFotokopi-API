using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.SellerDTOs;
using WebFotokopi.Application.ViewModels.Seller;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Application.Features.Commands.AppSellerCommands.CreateSeller
{
    public class CreateSellerCommandHandler : IRequestHandler<CreateSellerCommandRequest, CreateSellerCommandResponse>
    {
        private readonly ISellerService _sellerService;


        public CreateSellerCommandHandler(ISellerService sellerService)
        {
            _sellerService = sellerService;

        }

        public async Task<CreateSellerCommandResponse> Handle(CreateSellerCommandRequest request, CancellationToken cancellationToken)
        {
            CreateSellerDTO result = await _sellerService.CreateSellerAsync(new()
            {
                CompanyName = request.CompanyName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Password = request.Password,
                PasswordAgain = request.PasswordAgain,
                DistrictID = request.DistrictID,
                Address = request.Address
            });
            return new()
            {
                Succeeded =result.Succeeded,
                Message = result.Message,
            };
        }
    }
}
