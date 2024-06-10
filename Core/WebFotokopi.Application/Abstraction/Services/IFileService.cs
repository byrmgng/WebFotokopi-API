using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.FileDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.ViewModels.File;


namespace WebFotokopi.Application.Abstraction.Services
{
    public interface IFileService
    {
        Task<CreateFileResponseDTO> CreateFileAsync(VM_Create_File vmCreateFile);
        Task<IEnumerable<GetFileDTO>> GetFileAsync(VM_FilterGet_File vmFilterGetFile);
        Task<GetByIdFileDTO> GetByIdFileAsync(string id);
        Task<UpdateFileResponseDTO> UpdateFileAsync(VM_Update_File vmUpdateFile);
        Task<DeleteFileResponseDTO> DeleteFileAsync(string id);

        Task<IEnumerable<GetFileDTO>> GetFileForCustomerAsync(VM_FilterGet_File_ForCustomer vmFilterGetFileForCustomer);
        Task<GetByIdFileDTO> GetByIdFileForCustomerAsync(string id);
        Task<DeleteFileResponseDTO> DeleteFileForCustomerAsync(string id);

    }
}
