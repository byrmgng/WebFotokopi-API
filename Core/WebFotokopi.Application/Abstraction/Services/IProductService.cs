using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.FileDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.DTOs.ProductDTOs;
using WebFotokopi.Application.ViewModels.Package;
using WebFotokopi.Application.ViewModels.Product;

namespace WebFotokopi.Application.Abstraction.Services
{
    public interface IProductService
    {
        Task<CreateProductResponseDTO> CreateProductAsync(VM_Create_Product vmCreateProduct);
        Task<CreateProductResponseDTO> CreateProductForCustomerAsync(VM_CreateProduct_ForCustomer vmCreateProductForCustomer);
        Task<CreateFileResponseDTO> CreateFileForCustomerAsync(VM_CreateProduct_ForCustomer vmCreateProductForCustomer);
        Task<DeleteProductResponseDTO> DeleteProductAsync(string id);



    }
}
