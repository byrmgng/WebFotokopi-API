using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.CityDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.DTOs.SellerAddressDTOs;
using WebFotokopi.Application.ViewModels.Package;
using WebFotokopi.Application.ViewModels.SellerAddresses;

namespace WebFotokopi.Application.Abstraction.Services
{
    public interface IPackageService
    {
        Task<GetPackageFeaturesDTO> GetPackageFeatureAsync();
        Task<CreatePackageResponseDTO> CreatePackageAsync(VM_Create_Package vmCreatePackage);
        Task<IEnumerable<GetPackageDTO>> GetAllPackageAsync();
        Task<IEnumerable<GetPackageDTO>> GetFilterPackageAsync(VM_FilterGet_Package vM_FilterGet_Package);
        Task<IEnumerable<GetCustomersPackageDTO>> GetFilterCustomerPackageAsync(VM_FilterGet_Package_Customer vmFilterGetPackageCustomer);
        Task<DeletePackageResponseDTO> DeletePackageAsync(string id);
        Task<UpdatePackageResponseDTO> UpdatePackageAsync(VM_Update_Package vmUpdatePackage);

    }
}
