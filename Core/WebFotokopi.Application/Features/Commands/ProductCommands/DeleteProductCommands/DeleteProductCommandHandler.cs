using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.ProductDTOs;

namespace WebFotokopi.Application.Features.Commands.ProductCommands.DeleteProductCommands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            DeleteProductResponseDTO response = await _productService.DeleteProductAsync(request.ProductID);
            return (new() { Message = response.Message, Succeeded = response.Succeeded });
        }
    }
}
