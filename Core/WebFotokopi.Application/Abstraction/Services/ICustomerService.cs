using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.CustomerDTOs;
using WebFotokopi.Application.DTOs.SellerDTOs;
using WebFotokopi.Application.ViewModels.Customer;
using WebFotokopi.Application.ViewModels.Seller;

namespace WebFotokopi.Application.Abstraction.Services
{
    public interface ICustomerService
    {
        Task<CreateCustomerDTO> CreateCustomerAsync(VM_Create_Customer vmCreateSeller);
        Task<LoginCustomerDTO> LoginCustomerAsync(VM_Login_Customer vmLoginSeller);
        Task<LoginCustomerDTO> RefreshTokenLoginCustomerAsync(string token);
    }
}
