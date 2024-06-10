using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.ProductDTOs;

namespace WebFotokopi.Application.Features.Commands.ProductCommands.CreateProductForCustomerCommands
{
    public class CreateProductForCustomerCommandHandler:IRequestHandler<CreateProductForCustomerCommandRequest,CreateProductForCustomerCommandResponse>
    {
        private readonly IProductService _productService;

        public CreateProductForCustomerCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<CreateProductForCustomerCommandResponse> Handle(CreateProductForCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            CreateProductResponseDTO response = await _productService.CreateProductForCustomerAsync(new()
            {
                FileContent = request.FileContent,
                FileNote = request.FileNote,
                FileTitle = request.FileTitle,
                AppSellerID = request.AppSellerID,
                CustomerNote = request.CustomerNote,
                Quantity = request.Quantity,
                PackageID = request.PackageID,
            });
            return (new() { Message = response.Message, Succeeded = response.Succeeded });
        }
    }
}
