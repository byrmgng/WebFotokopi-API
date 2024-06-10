using Azure.Core;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.Abstraction.Tokens;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.DTOs.PaperSizeDTOs;
using WebFotokopi.Application.DTOs.PaperTypeDTOs;
using WebFotokopi.Application.DTOs.SellerDTOs;
using WebFotokopi.Application.DTOs.SheetsPerPageDTOs;
using WebFotokopi.Application.Features.Commands.AppSellerCommands.CreateSeller;
using WebFotokopi.Application.Features.Commands.SellerAddressCommands.CreateSellerAddress;
using WebFotokopi.Application.Repositories.ProductFeatureRepositories;
using WebFotokopi.Application.Repositories.SellerAddressRepositories;
using WebFotokopi.Application.ViewModels.File;
using WebFotokopi.Application.ViewModels.Package;
using WebFotokopi.Application.ViewModels.Seller;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Persistence.Services
{
    public class SellerService : ISellerService
    {
        readonly UserManager<AppSeller> _sellerUserManager;
        readonly IMediator _mediator;
        readonly SignInManager<AppSeller> _signInSellerManager;
        readonly ISellerTokenHandler _sellerTokenHandler;
        readonly IConfiguration _configuration;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppCustomer> _customerUserManager;
        readonly SignInManager<AppCustomer> _signInCustomerManager;



        public SellerService(SignInManager<AppCustomer> signInCustomerManager, UserManager<AppCustomer> customerUserManager, IHttpContextAccessor httpContextAccessor,UserManager<AppSeller> sellerUserManager, IMediator mediator, SignInManager<AppSeller> signInSellerManager, ISellerTokenHandler sellerTokenHandler,IConfiguration configuration)
        {
            _sellerUserManager = sellerUserManager;
            _mediator = mediator;
            _signInSellerManager = signInSellerManager;
            _sellerTokenHandler = sellerTokenHandler;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _customerUserManager = customerUserManager;
            _signInCustomerManager = signInCustomerManager;
        }
        public async Task<AppSeller?> FindSeller()
        {
            string? username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name?.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                AppSeller? appSeller = await _sellerUserManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
                return appSeller;
            }
            return null;
        }

        public async Task<CreateSellerDTO> CreateSellerAsync(VM_Create_Seller vmCreateSeller)
        {
            CreateSellerAddressCommandRequest request = new()
            {
                Address = vmCreateSeller.Address,
                DistrictID = vmCreateSeller.DistrictID,
            };
            CreateSellerAddressCommandResponse response =await _mediator.Send(request);
          
            IdentityResult result = await _sellerUserManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = vmCreateSeller.CompanyName,
                Email = vmCreateSeller.Email,
                PhoneNumber = vmCreateSeller.PhoneNumber,
                SellerAddressID = response.AddressID,
                UserName = vmCreateSeller.PhoneNumber,
                View = false
            }, vmCreateSeller.Password);
            bool success = (result.Succeeded) && response.Succeeded;
            string message = "Kullanıcı kaydı yapılamadı! ";
            if (!success)
                foreach (var error in result.Errors)
                    message += error.Description.ToString();
            return new()
            {
                Succeeded = success,
                Message = (success) ? "Kullanıcı kaydı başarılı bir şekilde yapıldı" : message
            };
        }

        public async Task<IEnumerable<ListSellerDTOs>> ListSellerAsync(VM_List_Seller vmListSeller)
        {
            string? username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name?.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                AppCustomer? appCustomer = await _customerUserManager.Users.Include(x=>x.CustomerAddress).FirstOrDefaultAsync(x => x.UserName == username);
                if (appCustomer != null)
                {
                    // Declare a variable to hold the result
                    return await _sellerUserManager.Users
                        .Include(x => x.SellerAddress)
                        .Include(x => x.Packages)
                        .Include(x => x.SellerAddress.District)
                        .Where(x => x.View == true)
                        .Where(x => x.SellerAddress.DistrictID == appCustomer.CustomerAddress.DistrictID)
                        .Where(x => string.IsNullOrEmpty(vmListSeller.SellerName) || x.CompanyName.Contains(vmListSeller.SellerName))
                        .Where(x => x.Packages.Any(p => string.IsNullOrEmpty(vmListSeller.PaperSizeID) || p.PaperSizeID == Guid.Parse(vmListSeller.PaperSizeID))) // Use Any() to check if any package matches the condition
                        .Where(x => x.Packages.Any(p => string.IsNullOrEmpty(vmListSeller.PaperTypeID) || p.PaperTypeID == Guid.Parse(vmListSeller.PaperTypeID)))
                        .Where(x => x.Packages.Any(p => string.IsNullOrEmpty(vmListSeller.SheetsPerPageID) || p.SheetsPerPageID == Guid.Parse(vmListSeller.SheetsPerPageID)))
                        .Where(x => x.Packages.Any(p => string.IsNullOrEmpty(vmListSeller.ColorMode) || p.ColorMode == (vmListSeller.ColorMode.ToLower() == "true")))
                        .Where(x => x.Packages.Any(p => string.IsNullOrEmpty(vmListSeller.DuplexMode) || p.DuplexMode == (vmListSeller.DuplexMode.ToLower() == "true")))
                        .Where(x => x.Packages.Any(p => p.isActive == true))
                        .Select(p => new ListSellerDTOs
                        {
                            SellerID = p.Id,
                            Address = p.SellerAddress.Address,
                            CompanyName = p.CompanyName,
                            DistrictName = p.SellerAddress.District.Name,
                            Logo = p.Logo,
                        }).ToListAsync();
                }
            }
            return new List<ListSellerDTOs>();
        }
        
        public async Task<LoginSellerDTO> LoginSellerAsync(VM_Login_Seller vmLoginSeller)
        {
            LoginSellerDTO loginSellerDTO = new();
            
            AppSeller? seller = await _sellerUserManager.FindByEmailAsync(vmLoginSeller.MailorPhoneNumber);
            if (seller == null)
                seller = await _sellerUserManager.FindByNameAsync(vmLoginSeller.MailorPhoneNumber);//Username telefon numarasına eşit
            if (seller == null)
            {
                loginSellerDTO.Succeeded = false;
                loginSellerDTO.Message = "Kullanıcı bulunamadı";
            }
            else
            {
                SignInResult result = await _signInSellerManager.CheckPasswordSignInAsync(seller, vmLoginSeller.Password, false);
                loginSellerDTO.Succeeded = result.Succeeded;
                if(result.Succeeded)
                {
                    loginSellerDTO.Message = "Hoşgeldin : " + seller.CompanyName;
                    loginSellerDTO.Token = _sellerTokenHandler.CreateAccessToken(seller);
                    seller.RefleshToken = loginSellerDTO.Token.RefreshToken;
                    seller.RefleshTokenEndDate = loginSellerDTO.Token.Expiration.AddMinutes(Convert.ToInt32(_configuration["SellerToken:LifeTimeMinute"]));
                    await _sellerUserManager.UpdateAsync(seller);
                }
                else
                {
                    loginSellerDTO.Message = "Şifre Hatalı";
                }
            }
            return loginSellerDTO;
        }

        public async Task<LoginSellerDTO> RefreshTokenLoginSellerAsync(string token)
        {
            LoginSellerDTO loginSellerDTO = new();
            AppSeller? seller = await _sellerUserManager.Users.FirstOrDefaultAsync(x=>x.RefleshToken == token);
            if (seller != null && seller?.RefleshTokenEndDate>DateTime.UtcNow)
            {
                loginSellerDTO.Token = _sellerTokenHandler.CreateAccessToken(seller);
                seller.RefleshToken = loginSellerDTO.Token.RefreshToken;
                seller.RefleshTokenEndDate = loginSellerDTO.Token.Expiration.AddMinutes(Convert.ToInt32(_configuration["SellerToken:LifeTimeMinute"]));
                await _sellerUserManager.UpdateAsync(seller);
                loginSellerDTO.Succeeded = true;
            }
            return loginSellerDTO;
        }

        public async Task<ListSellerDTOs> SellerFeaturesAsync(string id)
        {
            string? username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name?.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                AppCustomer? appCustomer = await _customerUserManager.Users.Include(x => x.CustomerAddress).FirstOrDefaultAsync(x => x.UserName == username);
                if (appCustomer != null)
                {
                    var sellers = await _sellerUserManager.Users
                        .Include(x => x.SellerAddress)
                        .Include(x => x.SellerAddress.District)
                        .Where(x => x.View == true)
                        .Where(x => x.SellerAddress.DistrictID == appCustomer.CustomerAddress.DistrictID)
                        .Where(x=>x.Id == id)
                        .Select(x => new ListSellerDTOs
                        {
                            SellerID = x.Id,
                            Address = x.SellerAddress.Address,
                            CompanyName = x.CompanyName,
                            DistrictName = x.SellerAddress.District.Name,
                            Logo = x.Logo2,
                        }).FirstOrDefaultAsync();
                    return sellers;
                }
            }
            return new ListSellerDTOs();
        }
        public async Task<GetSellerAccountInfoDTO> GetSellerAccountInfoAsync()
        {
            AppSeller appSeller = await FindSeller();
            if(appSeller != null)
            {
                var sellerInfo = await _sellerUserManager.Users
                    .Include(x => x.SellerAddress)
                    .ThenInclude(x => x.District)
                    .ThenInclude(x => x.City)
                    .Where(x=>x.Id == appSeller.Id)
                    .Select(x => new GetSellerAccountInfoDTO
                    {
                        CityID = x.SellerAddress.District.City.ID.ToString(),
                        DistrictID = x.SellerAddress.District.ID.ToString(),
                        Address = x.SellerAddress.Address,
                        CompanyName = x.CompanyName,
                        Description = x.Description,
                        PhoneNumber = x.PhoneNumber,
                    }).FirstOrDefaultAsync();
                return sellerInfo;
            }
            return new();
        }

        public async Task<UpdateSellerDTO> UpdateSellerAsync(VM_Update_Seller vmUpdateSeller)
        {
            AppSeller appSeller = await FindSeller();
            if (appSeller != null)
            {
                appSeller.CompanyName = vmUpdateSeller.CompanyName;
                appSeller.Description = vmUpdateSeller.Description;
                IdentityResult result = await _sellerUserManager.UpdateAsync(appSeller);
                if(result.Succeeded)
                    return new() { Message = "Güncelleme İşlemi Başarılı",Succeeded = true };
                return new() { Succeeded = false, Message = "Güncelleme İşlemi Başarısız" };
            }
            return new() { Message = "Kullanıcı doğrulanamadı",Succeeded=false};
        }
        public async Task<UpdateSellerDTO> UpdeteSellerLogoAsync(VM_Update_SellerLogo vmUpdateSellerLogo)
        {
            AppSeller appSeller = await FindSeller();
            if (appSeller != null)
            {
                if (vmUpdateSellerLogo.Logo == null || vmUpdateSellerLogo.Logo.Length == 0)
                    return new() { Message = "Dosya Bulunamadı", Succeeded = false };
                using (var stream = new MemoryStream())
                {
                    if (appSeller != null)
                    {
                        await vmUpdateSellerLogo.Logo.CopyToAsync(stream);
                        stream.Position = 0;
                        byte[] fileBytes = stream.ToArray();
                        string base64String = Convert.ToBase64String(fileBytes);
                        appSeller.Logo = base64String;
                        IdentityResult result = await _sellerUserManager.UpdateAsync(appSeller);
                        if (result.Succeeded)
                            return new() { Succeeded = true, Message = "Görsel ekleme işlemi başarı ile tamamlandı" };
                        return new() { Succeeded = false, Message = "Görsel ekleme işlemi tamamlanamadı!" };
                    }
                }
            }
            return new() { Message = "Kullanıcı doğrulanamadı", Succeeded = false };
        }
        public async Task<UpdateSellerDTO> UpdeteSellerLogo2Async(VM_Update_SellerLogo vmUpdateSellerLogo)
        {
            AppSeller appSeller = await FindSeller();
            if (appSeller != null)
            {
                if (vmUpdateSellerLogo.Logo == null || vmUpdateSellerLogo.Logo.Length == 0)
                    return new() { Message = "Dosya Bulunamadı", Succeeded = false };
                using (var stream = new MemoryStream())
                {
                    if (appSeller != null)
                    {
                        await vmUpdateSellerLogo.Logo.CopyToAsync(stream);
                        stream.Position = 0;
                        byte[] fileBytes = stream.ToArray();
                        string base64String = Convert.ToBase64String(fileBytes);
                        appSeller.Logo2 = base64String;
                        IdentityResult result = await _sellerUserManager.UpdateAsync(appSeller);
                        if (result.Succeeded)
                            return new() { Succeeded = true, Message = "Görsel ekleme işlemi başarı ile tamamlandı" };
                        return new() { Succeeded = false, Message = "Görsel ekleme işlemi tamamlanamadı!" };
                    }
                }
            }
            return new() { Message = "Kullanıcı doğrulanamadı", Succeeded = false };
        }
    }
}
