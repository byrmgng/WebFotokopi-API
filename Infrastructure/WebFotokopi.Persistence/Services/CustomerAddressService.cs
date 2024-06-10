using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.CustomerAddressDTOs;
using WebFotokopi.Application.DTOs.SellerAddressDTOs;
using WebFotokopi.Application.Repositories.CustomerAddressRepositories;
using WebFotokopi.Application.Repositories.SellerAddressRepositories;
using WebFotokopi.Application.ViewModels.CustomerAddress;
using WebFotokopi.Application.ViewModels.SellerAddresses;
using WebFotokopi.Domain.Entities;

namespace WebFotokopi.Persistence.Services
{
    public class CustomerAddressService:ICustomerAddressService
    {
        readonly ICustomerAddressWriteRepository _customerAddressWriteRepository;

        public CustomerAddressService(ICustomerAddressWriteRepository customerAddressWriteRepository)
        {
            _customerAddressWriteRepository = customerAddressWriteRepository;
        }

        public async Task<CreateCustomerAddressDTO> CreateCustomerAddressAsync(VM_Create_CustomerAddress customerAddress)
        {
            CustomerAddress _customerAdress = new()
            {
                ID = Guid.NewGuid(),
                Address = customerAddress.Address,
                DistrictID = customerAddress.DistrictID,
            };
            bool result = await _customerAddressWriteRepository.AddAsync(_customerAdress);
            await _customerAddressWriteRepository.SaveAsync();
            return new()
            {
                Id = _customerAdress.ID,
                Succeeded = result,
                Message = (result) ? "Adres kaydı başarılı" : "Adres kaydı başarısız"
            };
        }
    }
}
