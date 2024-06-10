using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.Abstraction.Tokens;
using WebFotokopi.Application.DTOs.CustomerDTOs;
using WebFotokopi.Application.Features.Commands.CustomerAddressCommands.CreateCustomerAddress;
using WebFotokopi.Application.Repositories.CustomerAddressRepositories;
using WebFotokopi.Application.ViewModels.Customer;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Persistence.Services
{
    public class CustomerService:ICustomerService
    {
        readonly UserManager<AppCustomer> _userManager;
        readonly IMediator _mediator;
        readonly SignInManager<AppCustomer> _signInManager;
        readonly ICustomerTokenHandler _customerTokenHandler;
        readonly IConfiguration _configuration;
        readonly ICustomerAddressReadRepository _customerAddressReadRepository;

        public CustomerService(ICustomerAddressReadRepository customerAddressReadRepository, UserManager<AppCustomer> userManager, IMediator mediator, SignInManager<AppCustomer> signInManager, ICustomerTokenHandler customerTokenHandler, IConfiguration configuration)
        {
            _userManager = userManager;
            _mediator = mediator;
            _signInManager = signInManager;
            _customerTokenHandler = customerTokenHandler;
            _configuration = configuration;
            _customerAddressReadRepository = customerAddressReadRepository;
        }

        public async Task<CreateCustomerDTO> CreateCustomerAsync(VM_Create_Customer vmCreateCustomer)
        {
            CreateCustomerAddressCommandRequest request = new()
            {
                Address = vmCreateCustomer.Address,
                DistrictID = vmCreateCustomer.DistrictID,
            };
            CreateCustomerAddressCommandResponse response = await _mediator.Send(request);

            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = vmCreateCustomer.FirstName,
                LastName = vmCreateCustomer.LastName,
                Email = vmCreateCustomer.Email,
                PhoneNumber = vmCreateCustomer.PhoneNumber,
                CustomerAddressID = response.AddressID,
                UserName = vmCreateCustomer.PhoneNumber
            }, vmCreateCustomer.Password);
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
        public async Task<LoginCustomerDTO> LoginCustomerAsync(VM_Login_Customer vmLoginCustomer)
        {
            LoginCustomerDTO loginCustomerDTO = new();

            AppCustomer? customer = await _userManager.FindByEmailAsync(vmLoginCustomer.MailorPhoneNumber);
            if (customer == null)
                customer = await _userManager.FindByNameAsync(vmLoginCustomer.MailorPhoneNumber);//Username telefon numarasına eşit
            if (customer == null)
            {
                loginCustomerDTO.Succeeded = false;
                loginCustomerDTO.Message = "Kullanıcı bulunamadı";
            }
            else
            {
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(customer, vmLoginCustomer.Password, false);
                loginCustomerDTO.Succeeded = result.Succeeded;
                if (result.Succeeded)
                {
                    loginCustomerDTO.Message = "Hoşgeldin : " + customer.FirstName + customer.LastName;
                    loginCustomerDTO.Token = _customerTokenHandler.CreateAccessToken(customer);
                    customer.RefleshToken = loginCustomerDTO.Token.RefreshToken;
                    customer.RefleshTokenEndDate = loginCustomerDTO.Token.Expiration.AddMinutes(Convert.ToInt32(_configuration["CustomerToken:LifeTimeMinute"]));
                    await _userManager.UpdateAsync(customer);
                }
                else
                {
                    loginCustomerDTO.Message = "Şifre Hatalı";
                }
            }
            return loginCustomerDTO;
        }

        public async Task<LoginCustomerDTO> RefreshTokenLoginCustomerAsync(string token)
        {
            LoginCustomerDTO loginCustomerDTO = new();
            AppCustomer? customer = await _userManager.Users.FirstOrDefaultAsync(x => x.RefleshToken == token);
            if (customer != null && customer?.RefleshTokenEndDate > DateTime.UtcNow)
            {
                loginCustomerDTO.Token = _customerTokenHandler.CreateAccessToken(customer);
                customer.RefleshToken = loginCustomerDTO.Token.RefreshToken;
                customer.RefleshTokenEndDate = loginCustomerDTO.Token.Expiration.AddMinutes(Convert.ToInt32(_configuration["CustomerToken:LifeTimeMinute"]));
                await _userManager.UpdateAsync(customer);
                loginCustomerDTO.Succeeded = true;
            }
            return loginCustomerDTO;
        }
    }
}
