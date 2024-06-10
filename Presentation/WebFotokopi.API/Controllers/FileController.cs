using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.FileDTOs;
using WebFotokopi.Application.Features.Commands.FileCommands.CreateFile;
using WebFotokopi.Application.Features.Commands.FileCommands.DeleteFile;
using WebFotokopi.Application.Features.Commands.FileCommands.UpdateFile;
using WebFotokopi.Application.Features.Commands.PackageCommands.DeletePackageCommands;
using WebFotokopi.Application.Features.Commands.PackageCommands.UpdatePackageCommands;
using WebFotokopi.Application.Features.Queries.FileQueries.GetByIdFile;
using WebFotokopi.Application.Features.Queries.FileQueries.GetByIdFileForCustomer;
using WebFotokopi.Application.Features.Queries.FileQueries.GetFilterFile;
using WebFotokopi.Application.Features.Queries.FileQueries.GetFilterFileForCustomer;

namespace WebFotokopi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        readonly IMediator _mediator;

        public FileController(IMediator mediator,IFileService fileService)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> CreateFile([FromForm] CreateFileCommandRequest createFileCommandRequest)
        {
            CreateFileCommandResponse createFileCommandResponse = await _mediator.Send(createFileCommandRequest);
            return Ok(createFileCommandResponse);
        }
        
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> GetFile(GetFilterFileQueryRequest getFilterFileQueryRequest)
        {
            GetFilterFileQueryResponse getFilterFileQueryResponse = await _mediator.Send(getFilterFileQueryRequest);
            return Ok(getFilterFileQueryResponse);
        }
       
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> GetByIdFile(GetByIdFileQueryRequest getByIdFileQueryRequest)
        {
            GetByIdFileQueryResponse getByIdFileQueryResponse = await _mediator.Send(getByIdFileQueryRequest);
            return Ok(getByIdFileQueryResponse);
        }
        

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> UpdateFile(UpdateFileCommandRequest updateFileCommandRequest)
        {
            UpdateFileCommandResponse updateFileCommandResponse = await _mediator.Send(updateFileCommandRequest);
            return Ok(updateFileCommandResponse);
        }
        
        [HttpDelete("[action]")]
        [Authorize(AuthenticationSchemes = "Seller")]
        public async Task<IActionResult> DeleteFile(string id)
        {
            DeleteFileCommandRequest deleteFileCommandRequest = new() { FileID = id };
            DeleteFileCommandResponse deleteFileCommandResponse = await _mediator.Send(deleteFileCommandRequest);
            return Ok(deleteFileCommandResponse);
        }
        
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IActionResult> GetFileForCustomer(GetFilterFileForCustomerQueryRequest getFilterFileForCustomerQueryRequest)
        {
            GetFilterFileForCustomerQueryResponse getFilterFileForCustomerQueryResponse = await _mediator.Send(getFilterFileForCustomerQueryRequest);
            return Ok(getFilterFileForCustomerQueryResponse);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IActionResult> GetByIdFileForCustomer(GetByIdFileForCustomerQueryRequest getByIdFileForCustomerQueryRequest)
        {
            GetByIdFileForCustomerQueryResponse getByIdFileForCustomerQueryResponse = await _mediator.Send(getByIdFileForCustomerQueryRequest);
            return Ok(getByIdFileForCustomerQueryResponse);
        }
        


    }
}
