using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.DistrictDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.DTOs.PaperSizeDTOs;
using WebFotokopi.Application.DTOs.PaperTypeDTOs;
using WebFotokopi.Application.DTOs.SheetsPerPageDTOs;
using WebFotokopi.Application.Repositories.DistrictRepositories;
using WebFotokopi.Application.Repositories.ProductFeatureRepositories;
using WebFotokopi.Application.Repositories.SellerPaperSizeRepositories;
using WebFotokopi.Application.Repositories.SellerPaperTypeRepositories;
using WebFotokopi.Application.Repositories.SellerSheetsPerPageRepositories;
using WebFotokopi.Application.ViewModels.Package;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Domain.Entities.Identity;
using WebFotokopi.Persistence.Repositories.DistrictRepositories;
using WebFotokopi.Persistence.Repositories.ProductFeatureRepositories;

namespace WebFotokopi.Persistence.Services
{
    public class PackageService : IPackageService
    {
        readonly IPackageWriteRepository _packageWriteRepository;
        readonly IPackageReadReposity _packageReadReposity;
        readonly ISheetsPerPageReadReposity _sheetsPerPageReadReposity;
        readonly IPaperSizeReadRepository _paperSizeReadRepository;
        readonly IPaperTypeReadRepository _paperTypeReadRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppSeller> _userManager;
        public PackageService(IPackageReadReposity packageReadReposity, UserManager<AppSeller> userManager, ISheetsPerPageReadReposity sheetsPerPageReadReposity, IPaperSizeReadRepository paperSizeReadRepository, IPaperTypeReadRepository paperTypeReadRepository,IHttpContextAccessor httpContextAccessor,IPackageWriteRepository packageWriteRepository)
        {
            _packageWriteRepository = packageWriteRepository;
            _packageReadReposity = packageReadReposity;
            _sheetsPerPageReadReposity = sheetsPerPageReadReposity;
            _paperSizeReadRepository = paperSizeReadRepository;
            _paperTypeReadRepository = paperTypeReadRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<AppSeller?> FindSeller()
        {
            string? username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name?.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                AppSeller? appSeller = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
                return appSeller;
            }
            return null;
        }
        public async Task<CreatePackageResponseDTO> CreatePackageAsync(VM_Create_Package vmCreatePackage)
        {
            AppSeller? appSeller = FindSeller().Result;
            if (appSeller != null)
            {
                Package package = new()
                {
                    ID = Guid.NewGuid(),
                    SellerID = appSeller.Id,    
                    Name = vmCreatePackage.Name,
                    Price = vmCreatePackage.Price,
                    ColorMode = vmCreatePackage.ColorMode,
                    SheetsPerPageID = new Guid(vmCreatePackage.SheetsPerPageID),
                    PaperSizeID = new Guid(vmCreatePackage.PaperSizeID),
                    PaperTypeID = new Guid(vmCreatePackage.PaperTypeID),
                    DuplexMode = vmCreatePackage.DuplexMode,
                    isActive = vmCreatePackage.isActive
                };

                bool succeeded = await _packageWriteRepository.AddAsync(package);
                await _packageWriteRepository.SaveAsync();
                if (succeeded)
                    return new() { Succeeded = succeeded, Message = "Paket ekleme işlemi başarı ile tamamlandı" };
                return new() { Succeeded = succeeded, Message = "Paket ekleme işlemi tamamlanamadı!" };
            }
            return new() { Succeeded = false, Message = "Paket ekleme işlemi için kullanıcı doğrulanamadı" };
        }

        public async Task<GetPackageFeaturesDTO> GetPackageFeatureAsync()
        {
            List<SheetsPerPage> sheetsPerPages = await _sheetsPerPageReadReposity.GetAll(false).ToListAsync();
            List<PaperSize> paperSizes = await _paperSizeReadRepository.GetAll(false).ToListAsync();
            List<PaperType> paperTypes = await _paperTypeReadRepository.GetAll(false).ToListAsync();
            return new GetPackageFeaturesDTO
            {
                PaperSizes = paperSizes.Select(paperSize => new ListPaperSizeDTO
                {
                    PaperSizeID = paperSize.ID,
                    PaperSizeName = paperSize.SizeName
                }),

                PaperTypes = paperTypes.Select(paperType => new ListPaperTypeDTO
                {
                    PaperTypeID = paperType.ID,
                    PaperTypeName = paperType.PaperTypeName
                }),

                SheetsPerPages = sheetsPerPages.Select(sheetsPerPage => new ListSheetsPerPageDTO
                {
                    SheetsPerPageID = sheetsPerPage.ID,
                    SheetsPerPageNumber = sheetsPerPage.SheetsPerPageNumber,
                })
            };
        }

        public async Task<IEnumerable<GetPackageDTO>> GetAllPackageAsync()
        {
            AppSeller? appSeller = FindSeller().Result;
            if (appSeller != null)
            {
                return await _packageReadReposity
                    .Table
                    .Include(p => p.PaperType)
                    .Include(p => p.SheetsPerPage)
                    .Include(p => p.PaperSize)
                    .Where(p => p.SellerID == appSeller.Id)
                    .Where(p=>p.View ==true)
                    .Select(p => new GetPackageDTO {
                        PackageID = p.ID,
                        PackageName = p.Name,
                        Price = p.Price,
                        DuplexMode = p.DuplexMode,
                        ColorMode = p.ColorMode,
                        isActive = p.isActive,
                        PaperType = new ListPaperTypeDTO
                        {
                            PaperTypeID = p.PaperType.ID,
                            PaperTypeName = p.PaperType.PaperTypeName
                        },
                        PaperSize = new ListPaperSizeDTO
                        {
                            PaperSizeID = p.PaperSize.ID,
                            PaperSizeName = p.PaperSize.SizeName
                        },
                        SheetsPerPage = new ListSheetsPerPageDTO
                        {
                            SheetsPerPageID = p.SheetsPerPage.ID,
                            SheetsPerPageNumber = p.SheetsPerPage.SheetsPerPageNumber
                        },
                        CreatedDate = p.CreatedDate.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss"),
                        UpdatedDate = p.UpdatedDate.HasValue ? p.UpdatedDate.Value.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss") : null

                    }).ToListAsync();

                 
            }
            return new List<GetPackageDTO>();
        }

        public async Task<DeletePackageResponseDTO> DeletePackageAsync(string id)
        {
            AppSeller? appSeller = FindSeller().Result;
            if (appSeller != null)
            {
                Package package = await _packageReadReposity.GetByIdAsync(id);
                if (string.Equals(appSeller.Id, package.SellerID)){
                    package.View = false;
                    package.DeletedDate = DateTime.UtcNow;
                    bool succeeded = _packageWriteRepository.Update(package);
                    await _packageWriteRepository.SaveAsync();
                    if (succeeded)
                        return new() { Succeeded = succeeded, Message = "Paket silme işlemi başarı ile tamamlandı" };
                }
                return new() { Succeeded = false, Message = "Paket silme işlemi sırasında kullanıcı doğrulanamadı" };
            }
            return new() { Succeeded = false , Message="Paket silme işlemi sırasında kullanıcı doğrulanamadı"};
        }

        public async Task<UpdatePackageResponseDTO> UpdatePackageAsync(VM_Update_Package vmUpdatePackage)
        {
            AppSeller? appSeller = FindSeller().Result;
            if (appSeller != null)
            {
                Package package = await _packageReadReposity.GetByIdAsync(vmUpdatePackage.PackageId);
                if (string.Equals(appSeller.Id, package.SellerID))
                {
                    package.Name = vmUpdatePackage.Name;
                    package.SheetsPerPageID = new Guid(vmUpdatePackage.SheetsPerPageID);
                    package.PaperTypeID = new Guid(vmUpdatePackage.PaperTypeID);
                    package.PaperSizeID = new Guid(vmUpdatePackage.PaperSizeID);
                    package.ColorMode = vmUpdatePackage.ColorMode;
                    package.DuplexMode = vmUpdatePackage.DuplexMode;
                    package.Price = vmUpdatePackage.Price;
                    package.isActive = vmUpdatePackage.isActive;


                    bool succeeded = _packageWriteRepository.Update(package);
                    await _packageWriteRepository.SaveAsync();
                    if (succeeded)
                        return new() { Succeeded = succeeded, Message = "Paket güncelleme işlemi başarı ile tamamlandı" };
                }
                return new() { Succeeded = false, Message = "Paket güncelleme işlemi sırasında kullanıcı doğrulanamadı" };
            }
            return new() { Succeeded = false, Message = "Paket güncelleme işlemi sırasında kullanıcı doğrulanamadı" };
        }

        public async Task<IEnumerable<GetPackageDTO>> GetFilterPackageAsync(VM_FilterGet_Package vM_FilterGet_Package)
        {
            AppSeller? appSeller = FindSeller().Result;
            if (appSeller != null)
            {
                List<Package> packages = await _packageReadReposity
                .Table
                .Include(p => p.PaperType)
                .Include(p => p.SheetsPerPage)
                .Include(p => p.PaperSize)
                .Where(p => p.SellerID == appSeller.Id)
                .Where(p => p.View)
                .Where(p => string.IsNullOrEmpty(vM_FilterGet_Package.SheetsPerPageID) || p.SheetsPerPageID == Guid.Parse(vM_FilterGet_Package.SheetsPerPageID))
                .Where(p => string.IsNullOrEmpty(vM_FilterGet_Package.PaperTypeID) || p.PaperTypeID == Guid.Parse(vM_FilterGet_Package.PaperTypeID))
                .Where(p => string.IsNullOrEmpty(vM_FilterGet_Package.PaperSizeID) || p.PaperSizeID == Guid.Parse(vM_FilterGet_Package.PaperSizeID))
                .Where(p => string.IsNullOrEmpty(vM_FilterGet_Package.ColorMode) || p.ColorMode == (vM_FilterGet_Package.ColorMode.ToLower() == "true"))
                .Where(p => string.IsNullOrEmpty(vM_FilterGet_Package.DuplexMode) || p.DuplexMode == (vM_FilterGet_Package.DuplexMode.ToLower() == "true"))
                .Where(p => string.IsNullOrEmpty(vM_FilterGet_Package.IsActive) || p.isActive == (vM_FilterGet_Package.IsActive.ToLower() == "true"))
                .Where(p => string.IsNullOrEmpty(vM_FilterGet_Package.PackageName) || p.Name.Contains(vM_FilterGet_Package.PackageName))
                .ToListAsync();

                List<GetPackageDTO> packageDTOs = packages.Select(p => new GetPackageDTO
                {
                    PackageID = p.ID,
                    PackageName = p.Name,
                    Price = p.Price,
                    DuplexMode = p.DuplexMode,
                    ColorMode = p.ColorMode,
                    isActive = p.isActive,
                    PaperType = new ListPaperTypeDTO
                    {
                        PaperTypeID = p.PaperType.ID,
                        PaperTypeName = p.PaperType?.PaperTypeName
                    },
                    PaperSize = new ListPaperSizeDTO
                    {
                        PaperSizeID = p.PaperSize.ID,
                        PaperSizeName = p.PaperSize?.SizeName
                    },
                    SheetsPerPage = new ListSheetsPerPageDTO
                    {
                        SheetsPerPageID = p.SheetsPerPage.ID,
                        SheetsPerPageNumber = p.SheetsPerPage.SheetsPerPageNumber
                    },
                    CreatedDate = p.CreatedDate.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss"),
                    UpdatedDate = p.UpdatedDate?.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss")

                }).ToList();

                return packageDTOs;
            }
            return new List<GetPackageDTO>();
        }

        public async Task<IEnumerable<GetCustomersPackageDTO>> GetFilterCustomerPackageAsync(VM_FilterGet_Package_Customer vmFilterGetPackageCustomer)
        {
            string? username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name?.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                return await _packageReadReposity
                .Table
                .Include(p => p.PaperType)
                .Include(p => p.SheetsPerPage)
                .Include(p => p.PaperSize)
                .Include(p => p.AppSeller)
                .Where(p => p.View)
                .Where(p => p.isActive == true)
                .Where(p => p.SellerID == vmFilterGetPackageCustomer.SellerID)
                .Where(p => string.IsNullOrEmpty(vmFilterGetPackageCustomer.SheetsPerPageID) || p.SheetsPerPageID == Guid.Parse(vmFilterGetPackageCustomer.SheetsPerPageID))
                .Where(p => string.IsNullOrEmpty(vmFilterGetPackageCustomer.PaperTypeID) || p.PaperTypeID == Guid.Parse(vmFilterGetPackageCustomer.PaperTypeID))
                .Where(p => string.IsNullOrEmpty(vmFilterGetPackageCustomer.PaperSizeID) || p.PaperSizeID == Guid.Parse(vmFilterGetPackageCustomer.PaperSizeID))
                .Where(p => string.IsNullOrEmpty(vmFilterGetPackageCustomer.ColorMode) || p.ColorMode == (vmFilterGetPackageCustomer.ColorMode.ToLower() == "true"))
                .Where(p => string.IsNullOrEmpty(vmFilterGetPackageCustomer.DuplexMode) || p.DuplexMode == (vmFilterGetPackageCustomer.DuplexMode.ToLower() == "true"))
                .Where(p => string.IsNullOrEmpty(vmFilterGetPackageCustomer.PackageName) || p.Name.Contains(vmFilterGetPackageCustomer.PackageName))
                .Select(p => new GetCustomersPackageDTO
                {
                    PackageID = p.ID,
                    PackageName = p.Name,
                    Price = p.Price,
                    DuplexMode = p.DuplexMode,
                    ColorMode = p.ColorMode,
                    PaperType = new ListPaperTypeDTO
                    {
                        PaperTypeID = p.PaperType.ID,
                        PaperTypeName = p.PaperType.PaperTypeName
                    },
                    PaperSize = new ListPaperSizeDTO
                    {
                        PaperSizeID = p.PaperSize.ID,
                        PaperSizeName = p.PaperSize.SizeName
                    },
                    SheetsPerPage = new ListSheetsPerPageDTO
                    {
                        SheetsPerPageID = p.SheetsPerPage.ID,
                        SheetsPerPageNumber = p.SheetsPerPage.SheetsPerPageNumber
                    },
                }).ToListAsync();

                 
            }
            return new List<GetCustomersPackageDTO>();
        }
    }
}
