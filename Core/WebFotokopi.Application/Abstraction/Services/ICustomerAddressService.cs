using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.CustomerAddressDTOs;
using WebFotokopi.Application.DTOs.SellerAddressDTOs;
using WebFotokopi.Application.ViewModels.CustomerAddress;
using WebFotokopi.Application.ViewModels.SellerAddresses;

namespace WebFotokopi.Application.Abstraction.Services
{
    public interface ICustomerAddressService
    {
        Task<CreateCustomerAddressDTO> CreateCustomerAddressAsync(VM_Create_CustomerAddress vmCustomerAddress);
    }
}
