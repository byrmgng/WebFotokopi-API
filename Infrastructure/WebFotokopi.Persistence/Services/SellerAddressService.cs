using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.DistrictDTOs;
using WebFotokopi.Application.DTOs.SellerAddressDTOs;
using WebFotokopi.Application.Repositories.SellerAddressRepositories;
using WebFotokopi.Application.ViewModels.SellerAddress;
using WebFotokopi.Application.ViewModels.SellerAddresses;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Persistence.Services
{
    public class SellerAddressService : ISellerAddressService
    {
        readonly ISellerAddressWriteRepository _sellerAddressWriteRepository;
        readonly ISellerAddressReadRepository _sellerAddressReadRepository;
        readonly IHttpContextAccessor _contextAccessor;
        readonly UserManager<AppSeller> _sellerUserManager;
        public SellerAddressService(ISellerAddressReadRepository sellerAddressReadRepository,UserManager<AppSeller> sellerUserManager,IHttpContextAccessor httpContextAccessor, ISellerAddressWriteRepository sellerAddressWriteRepository)
        {
            _sellerAddressWriteRepository = sellerAddressWriteRepository;
            _contextAccessor = httpContextAccessor;
            _sellerUserManager = sellerUserManager;
            _sellerAddressReadRepository = sellerAddressReadRepository;
        }

        public async Task<AppSeller?> FindSeller()
        {
            string? username = _contextAccessor?.HttpContext?.User?.Identity?.Name?.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                AppSeller? appSeller = await _sellerUserManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
                return appSeller;
            }
            return null;
        }

        public async Task<CreateSellerAddressDTO> CreateSellerAddressAsync(VM_Create_SellerAddress sellerAddress)
        {
            SellerAddress _sellerAdress = new()
            {
                ID = Guid.NewGuid(),
                Address = sellerAddress.Address,
                DistrictID = sellerAddress.DistrictID,
            };
            bool result =  await _sellerAddressWriteRepository.AddAsync(_sellerAdress);
            await _sellerAddressWriteRepository.SaveAsync();
            return new()
            {
                Id = _sellerAdress.ID,
                Succeeded = result,
                Message = (result) ? "Adres kaydı başarılı" : "Adres kaydı başarısız"
            };
        }

        public async Task<UpdateSellerAddressDTO> UpdateSellerAddress(VM_Update_SellerAddress vmUpdateSellerAddress)
        {
            AppSeller appSeller = await FindSeller();
            if (appSeller != null)
            {
                SellerAddress sellerAddress = await _sellerAddressReadRepository.GetByIdAsync(appSeller.SellerAddressID.ToString());
                sellerAddress.DistrictID = vmUpdateSellerAddress.DistrictID;
                sellerAddress.Address = vmUpdateSellerAddress.Address;
                bool success = _sellerAddressWriteRepository.Update(sellerAddress);
                await _sellerAddressWriteRepository.SaveAsync();
                if (success)
                    return new() { Succeeded = true, Message = "Adres Güncelleme Başarılı" };
                else
                    return new() { Succeeded = false, Message = "Ürün Güncellenemedi" };
            }
            return new() { Succeeded = false, Message = "Kullanıcı Doğrulanamadı" };
        }
    }
}
