using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.DTOs.ProductDTOs;

namespace WebFotokopi.Application.Features.Commands.ProductCommands.CreateProductCommands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            CreateProductResponseDTO response = await _productService.CreateProductAsync( new()
            {
                FileID = request.FileID,
                CustomerNote = request.CustomerNote,
                Quantity = request.Quantity,
                PackageID = request.PackageID,
            });
            return (new() { Message = response.Message, Succeeded = response.Succeeded });
        }
    }
}
