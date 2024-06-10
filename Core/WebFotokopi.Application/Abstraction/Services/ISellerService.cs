using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.SellerAddressDTOs;
using WebFotokopi.Application.DTOs.SellerDTOs;
using WebFotokopi.Application.ViewModels.Seller;
using WebFotokopi.Application.ViewModels.SellerAddress;
using WebFotokopi.Application.ViewModels.SellerAddresses;

namespace WebFotokopi.Application.Abstraction.Services
{
    public interface ISellerService
    {
        Task<CreateSellerDTO> CreateSellerAsync(VM_Create_Seller vmCreateSeller);
        Task<LoginSellerDTO> LoginSellerAsync(VM_Login_Seller vmLoginSeller);
        Task<LoginSellerDTO> RefreshTokenLoginSellerAsync(string token);
        Task<IEnumerable<ListSellerDTOs>> ListSellerAsync(VM_List_Seller vmListSeller);
        Task<ListSellerDTOs> SellerFeaturesAsync(string id);
        Task<GetSellerAccountInfoDTO> GetSellerAccountInfoAsync();
        Task<UpdateSellerDTO> UpdateSellerAsync(VM_Update_Seller vmUpdateSeller);
        Task<UpdateSellerDTO> UpdeteSellerLogoAsync(VM_Update_SellerLogo vmUpdateSellerLogo);
        Task<UpdateSellerDTO> UpdeteSellerLogo2Async(VM_Update_SellerLogo vmUpdateSellerLogo);

    }
}
