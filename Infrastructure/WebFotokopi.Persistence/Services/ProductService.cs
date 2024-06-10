using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.FileDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.DTOs.ProductDTOs;
using WebFotokopi.Application.Repositories.FileRepositories;
using WebFotokopi.Application.Repositories.OrderRepositories;
using WebFotokopi.Application.Repositories.ProductFeatureRepositories;
using WebFotokopi.Application.Repositories.ProductRepositories;
using WebFotokopi.Application.ViewModels.Product;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Persistence.Services
{
    public class ProductService : IProductService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppCustomer> _customerUserManager;
        readonly UserManager<AppSeller> _sellerUserManager;
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        readonly IPackageReadReposity _packageReadReposity;
        readonly IFileReadRepository _fileReadRepository;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IOrderService  _orderService;
        readonly IFileService _fileService;

        public ProductService(IFileService fileService,IProductReadRepository productReadRepository,IOrderService orderService,IFileWriteRepository fileWriteRepository, UserManager<AppSeller> sellerUserManager, IFileReadRepository fileReadRepository, IPackageReadReposity packageReadReposity, IHttpContextAccessor httpContextAccessor, UserManager<AppCustomer> customerUserManager, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _sellerUserManager = sellerUserManager;
            _customerUserManager = customerUserManager;
            _httpContextAccessor = httpContextAccessor;
            _productWriteRepository = productWriteRepository;
            _packageReadReposity = packageReadReposity;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _orderService = orderService;
            _fileService = fileService;
        }
        public async Task<AppCustomer?> FindCustomer()
        {
            string? username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name?.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                AppCustomer? appCustomer = await _customerUserManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
                return appCustomer;
            }
            return null;
        }
        public async Task<CreateProductResponseDTO> CreateProductAsync(VM_Create_Product vmCreateProduct)
        {
            AppCustomer? appCustomer = FindCustomer().Result;
            if (appCustomer != null)
            {
                var package = await _packageReadReposity.Table.Include(x => x.SheetsPerPage).Where(p => p.ID == new Guid(vmCreateProduct.PackageID)).FirstOrDefaultAsync();
                WebFotokopi.Domain.Entities.File file = await _fileReadRepository.GetByIdAsync(vmCreateProduct.FileID);
                if (package != null && file != null && (file.AppCustomerID != null) ? file.AppCustomerID == appCustomer.Id : true)
                {
                    Guid orderID = await _orderService.FindCustomerOrderIDAsync();
                    int count = file.NumberOfPage;
                    if (package.DuplexMode)
                    {
                        count = (file.NumberOfPage % 2 == 0) ? (file.NumberOfPage / 2) : ((file.NumberOfPage + 1) / 2);
                    }
                    count = !(count % package.SheetsPerPage.SheetsPerPageNumber > 0) ? (count / package.SheetsPerPage.SheetsPerPageNumber) : ((count / package.SheetsPerPage.SheetsPerPageNumber) + 1);
                    Product product = new();
                    product.PackageID = new Guid(vmCreateProduct.PackageID);
                    product.FileID = new Guid(vmCreateProduct.FileID);
                    product.Price = package.Price * count * vmCreateProduct.Quantity;
                    product.CustomerNote = vmCreateProduct.CustomerNote;
                    product.Quantity = vmCreateProduct.Quantity;
                    product.OrderID = orderID;
                    bool succeeded = await _productWriteRepository.AddAsync(product);
                    await _productWriteRepository.SaveAsync();
                    await _orderService.UpdateOrderPriceAsync();
                    if (succeeded)
                        return new() { Succeeded = succeeded, Message = "Ürün sepete eklendi" };
                    return new() { Succeeded = succeeded, Message = "Ürün sepete eklenemedi" };
                }
                return new() { Succeeded = false, Message = "Dosya yada paket bulunamadı" };
            }
            return new() { Succeeded = false, Message = "Ürünü sepete ekleme işlemi için kullanıcı doğrulanamadı" };
        }
        public async Task<CreateProductResponseDTO> CreateProductForCustomerAsync(VM_CreateProduct_ForCustomer vmCreateProductForCustomer)  
        {
            AppCustomer? appCustomer = FindCustomer().Result;
            if (appCustomer != null)
            {
                AppSeller? appSeller = await _sellerUserManager.Users.Where(x => x.Id == vmCreateProductForCustomer.AppSellerID).FirstOrDefaultAsync();
                if (vmCreateProductForCustomer.FileContent == null || vmCreateProductForCustomer.FileContent.Length == 0)
                    return new() { Message = "Dosya Bulunamadı", Succeeded = false };
                CreateFileResponseDTO result = await CreateFileForCustomerAsync(vmCreateProductForCustomer);
                if(!result.Succeeded)
                    return new() { Message = result.Message, Succeeded = false };
                WebFotokopi.Domain.Entities.File file = await _fileReadRepository.GetByIdAsync(result.Message);
                var package = await _packageReadReposity.Table.Include(x => x.SheetsPerPage).Where(p => p.ID == new Guid(vmCreateProductForCustomer.PackageID)).FirstOrDefaultAsync();

                if (package != null || file != null)
                {
                    Guid orderID = await _orderService.FindCustomerOrderIDAsync();
                    int count = file.NumberOfPage;
                    if (package.DuplexMode)
                    {
                        count = (file.NumberOfPage % 2 == 0) ? (file.NumberOfPage / 2) : ((file.NumberOfPage + 1) / 2);
                    }
                    count = (count / package.SheetsPerPage.SheetsPerPageNumber == 1) ? (count / package.SheetsPerPage.SheetsPerPageNumber) : ((count / package.SheetsPerPage.SheetsPerPageNumber) + 1);
                    Product product = new();
                    product.PackageID = new Guid(vmCreateProductForCustomer.PackageID);
                    product.FileID = new Guid(result.Message);
                    product.Price = package.Price * count * vmCreateProductForCustomer.Quantity;
                    product.CustomerNote = vmCreateProductForCustomer.CustomerNote;
                    product.Quantity = vmCreateProductForCustomer.Quantity;
                    product.OrderID = orderID;
                    bool succeeded = await _productWriteRepository.AddAsync(product);
                    await _productWriteRepository.SaveAsync();
                    await _orderService.UpdateOrderPriceAsync();
                    if (succeeded)
                        return new() { Succeeded = succeeded, Message = "Ürün sepete eklendi" };
                    return new() { Succeeded = succeeded, Message = "Ürün sepete eklenemedi" };
                }
                return new() { Succeeded = false, Message = "Dosya yada paket bulunamadı" };
            }
            return new() { Succeeded = false, Message = "Ürünü sepete ekleme işlemi için kullanıcı doğrulanamadı" };
        }
        public async Task<CreateFileResponseDTO> CreateFileForCustomerAsync(VM_CreateProduct_ForCustomer vmCreateProductForCustomer)
        {
            AppCustomer? appCustomer = FindCustomer().Result;
            if (appCustomer != null)
            {
                AppSeller? appSeller = await _sellerUserManager.Users.Where(x => x.Id == vmCreateProductForCustomer.AppSellerID).FirstOrDefaultAsync();
                using (var stream = new MemoryStream())
                {
                    if (appSeller != null)
                    {
                        await vmCreateProductForCustomer.FileContent.CopyToAsync(stream);
                        stream.Position = 0;
                        byte[] fileBytes = stream.ToArray();
                        string base64String = Convert.ToBase64String(fileBytes);
                        int NumberOfPages;
                        using (PdfReader reader = new PdfReader(stream))
                        {
                            NumberOfPages = reader.NumberOfPages;
                        }
                        
                        WebFotokopi.Domain.Entities.File productFile = new()
                        {
                            ID = Guid.NewGuid(),
                            Title = vmCreateProductForCustomer.FileTitle,
                            Note = vmCreateProductForCustomer.FileNote,
                            FileContent = base64String,
                            FileName = vmCreateProductForCustomer.FileContent.FileName,
                            ContentType = vmCreateProductForCustomer.FileContent.ContentType,
                            AppSellerID = appSeller.Id,
                            SellerOwner = false,
                            NumberOfPage = NumberOfPages,
                            AppCustomerID = appCustomer.Id,
                        };
                        await _fileWriteRepository.AddAsync(productFile);
                        await _fileWriteRepository.SaveAsync();
                        return new() { Message = productFile.ID.ToString() , Succeeded = true };
                    }
                    return new() { Message = "Satıcı bulunamadı",Succeeded=false };
                }
            }
            return new() { Message = "Kullanıcı bulunamadı",Succeeded=false };
        }
        public async Task<DeleteProductResponseDTO> DeleteProductAsync(string id)
        {
            try
            {
                string fileID = _productReadRepository.GetByIdAsync(id).Result.FileID.ToString();
                bool sellerOrCustomer = await _fileReadRepository.Table.Where(x => x.ID.ToString() == fileID).Select(x => x.AppCustomerID != null ? true : false).FirstOrDefaultAsync();
                if (sellerOrCustomer)
                {
                    await _fileService.DeleteFileForCustomerAsync(fileID);
                    await _productWriteRepository.SaveAsync();
                }
                else
                {
                    await _productWriteRepository.RemoveAsync(id);
                    await _productWriteRepository.SaveAsync();
                }
                await _orderService.UpdateOrderPriceAsync();
                return new() { Message = "Ürün Silme İşlemi Başarı İle Tamamlandı", Succeeded = true };
            }
            catch (Exception ex)
            {
                return new() { Message="Ürün Silme İşlemi Sırasında Bir Hata İle Karşılaşıldı",Succeeded=false};
            }  
        }
    }
}
