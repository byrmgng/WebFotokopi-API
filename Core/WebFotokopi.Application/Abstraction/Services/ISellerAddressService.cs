using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.DistrictDTOs;
using WebFotokopi.Application.DTOs.SellerAddressDTOs;
using WebFotokopi.Application.ViewModels.SellerAddress;
using WebFotokopi.Application.ViewModels.SellerAddresses;

namespace WebFotokopi.Application.Abstraction.Services
{
    public interface ISellerAddressService
    {
        Task<CreateSellerAddressDTO> CreateSellerAddressAsync(VM_Create_SellerAddress vmSellerAddress);
        Task<UpdateSellerAddressDTO> UpdateSellerAddress(VM_Update_SellerAddress vmUpdateSellerAddress);

    }
}
