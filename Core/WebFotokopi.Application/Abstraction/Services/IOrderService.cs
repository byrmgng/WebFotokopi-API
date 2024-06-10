using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.OrderDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.ViewModels.Order;
using WebFotokopi.Application.ViewModels.Package;

namespace WebFotokopi.Application.Abstraction.Services
{
    public interface IOrderService
    {
        Task<GetOrderItems> GetOrderAsync();
        Task<Guid> FindCustomerOrderIDAsync();
        Task UpdateOrderPriceAsync();
        Task<PlaceOrderDTO> PlaceOrderAsync();
        Task<IEnumerable<GetOrderDetails>> GetOrderDetailsAsync();
        Task<IEnumerable<GetOrderDetailsForSeller>> GetOrderDetailsForSellerAsync();
        Task<IEnumerable<GetOrderDetailsForSeller>> GetOrderDetailsForSellerFilterAsync(VM_FilterGet_Order vmFilterGetOrder);
        Task<UpdateOrderStatusDTO> UpdateOrderStatusAsync(VM_Update_Order_Status vmUpdateOrderStatus);

    }
}
