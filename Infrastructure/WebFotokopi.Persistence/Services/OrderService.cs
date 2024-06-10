using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.OrderDTOs;
using WebFotokopi.Application.Repositories.OrderRepositories;
using WebFotokopi.Application.Repositories.ProductRepositories;
using WebFotokopi.Application.ViewModels.Order;
using WebFotokopi.Application.ViewModels.Package;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Domain.Entities.Identity;
using WebFotokopi.Persistence.Repositories.OrderRepositories;
using WebFotokopi.Persistence.Repositories.ProductRepositories;

namespace WebFotokopi.Persistence.Services
{
    public class OrderService:IOrderService
    {
        readonly IOrderReadRepository _orderReadRepository;
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppCustomer> _customerUserManager;
        readonly UserManager<AppSeller> _sellerUserManager;
        readonly IProductReadRepository _productReadRepository;


        public OrderService(UserManager<AppSeller> sellerUserManager,IProductReadRepository productReadRepository,UserManager<AppCustomer> customerUserManager, IHttpContextAccessor httpContextAccessor, IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _httpContextAccessor = httpContextAccessor;
            _customerUserManager = customerUserManager;
            _productReadRepository = productReadRepository;
            _sellerUserManager = sellerUserManager;
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

        public async Task<Guid> CreateOrderAsync(string customerID)
        {
            Order order = new()
            {
                ID = Guid.NewGuid(),
                CustomerID = customerID,
                Price = 0,
                Status = 0,
            };
            await _orderWriteRepository.AddAsync(order);
            await _orderWriteRepository.SaveAsync();
            return order.ID;
        }
        public async Task<Guid> FindCustomerOrderIDAsync()
        {
            AppCustomer? appCustomer =  await FindCustomer();
            Guid orderID;
            orderID = await _orderReadRepository.Table.Include(x => x.AppCustomer).Where(p => p.Status == 0).Where(x => x.AppCustomer.Id == appCustomer.Id).Select(x => x.ID).FirstOrDefaultAsync();
            if(orderID == Guid.Empty)
            {
                orderID = await CreateOrderAsync(appCustomer.Id);
            }
            return orderID;
        }

        public async Task<GetOrderItems> GetOrderAsync()
        {
            AppCustomer? appCustomer = await FindCustomer();
            if(appCustomer != null)
            {
                var orderInfo = await _orderReadRepository
                .Table
                .Include(x => x.Products)  // Include products in the order
                    .ThenInclude(p => p.File) // Include file within each product
                        .ThenInclude(f => f.AppSeller) // Include seller within each file
                .Where(x => x.AppCustomer.Id == appCustomer.Id)
                .Where(x => x.Status == 0)
                .Select(x => new GetOrderItems
                {
                    SellerName = x.Products.Any() ? x.Products.First().File.AppSeller.CompanyName : null,
                    Price = x.Price,
                    Items = x.Products.Select(product => new GetOrderItem
                    {
                        ProductID = product.ID,
                        Price = product.Price/product.Quantity,
                        NumberOfPage = product.File.NumberOfPage,
                        TotalPrice = product.Price,
                        Quantity = product.Quantity,
                        FileTitle = product.File.Title,
                    }).ToList()
                }).FirstOrDefaultAsync();
                if (orderInfo != null)
                    return orderInfo;
                else 
                    return new(){};

            }
            return new();
        }

        public async Task UpdateOrderPriceAsync()
        {
            List<Product> products = await _productReadRepository.GetAll().Where(x => x.OrderID == FindCustomerOrderIDAsync().Result).ToListAsync();
            float price = products.Sum(x => x.Price);
            Order order = await _orderReadRepository.GetByIdAsync(FindCustomerOrderIDAsync().Result.ToString());
            order.Price = price;
            _orderWriteRepository.Update(order);
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<PlaceOrderDTO> PlaceOrderAsync()
        {
            Guid orderID = await FindCustomerOrderIDAsync();
            if(Guid.Empty != orderID)
            {
                Order order = await _orderReadRepository.GetByIdAsync(orderID.ToString());
                order.Status = 1;
                order.CreatedDate = DateTime.UtcNow;
                order.SellerID = _productReadRepository.Table.Include(x=>x.Package).Where(x=>x.OrderID == order.ID).Select(x=>x.Package.SellerID).FirstOrDefault();
                bool success = _orderWriteRepository.Update(order);
                await _orderWriteRepository.SaveAsync();
                if (success)
                    return new() { Message = "Sipariş Oluşturuldu",Succeeded = success};
                else
                    return new() { Message = "Sipariş Oluşturulamadı", Succeeded = success };

            }
            return new() { Message = "Sipariş Bilgileri bulunamadı", Succeeded=false };
        }

        public async Task<IEnumerable<GetOrderDetails>> GetOrderDetailsAsync()
        {

            AppCustomer? appCustomer = await FindCustomer();
            if (appCustomer != null)
            {
                var orderInfo = await _orderReadRepository
                .Table
                .Where(x => x.AppCustomer.Id == appCustomer.Id)
                .Where(x => x.Status > 0)
                .Include(x => x.Products)
                    .ThenInclude(p => p.File)
                        .ThenInclude(f => f.AppSeller)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Package)
                        .ThenInclude(x => x.SheetsPerPage)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Package)
                        .ThenInclude(x => x.PaperSize)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Package)
                        .ThenInclude(x => x.PaperType)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new GetOrderDetails
                {
                    OrderID = x.ID.ToString(),
                    CompanyName = x.AppSeller.CompanyName,
                    CreatedDate = x.CreatedDate.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss"),
                    DeliveryDate = x.UpdatedDate.HasValue && x.Status == 4 ? x.UpdatedDate.Value.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss") : null,
                    OrderStatus = x.Status.ToString(),
                    Price = x.Price.ToString(),
                    OrderItems = x.Products.Select(product => new GetOrderDetailsItem
                    {
                        FileTitle = product.File.Title,
                        FileNote = product.File.Note,
                        FileNumberOfFile = product.File.NumberOfPage.ToString(),
                        Quantity = product.Quantity.ToString(),
                        Price = product.Price.ToString(),
                        PackagePrice = product.Package.Price.ToString(),
                        PackageColorMode = product.Package.ColorMode ? "Renkli" : "Siyah-Beyaz",
                        PackageDuplexMode = product.Package.DuplexMode ? "Arkalı-Önlü" : "Tek Yüz",
                        PackageSheetsPerPage = product.Package.SheetsPerPage.SheetsPerPageNumber.ToString(),
                        PackagePageSize = product.Package.PaperSize.SizeName,
                        PackagePaperType = product.Package.PaperType.PaperTypeName
                    }).ToList()
                }).ToListAsync();
                if (orderInfo != null)
                    return orderInfo;
                else
                    return new List<GetOrderDetails>();
            }
            return new List<GetOrderDetails>();
        }
        public async Task<IEnumerable<GetOrderDetailsForSeller>> GetOrderDetailsForSellerAsync()
        {
            AppSeller? appSeller = await FindSeller();
            if (appSeller != null)
            {
                var orderInfo = await _orderReadRepository
                .Table
                .Where(x => x.AppSeller.Id == appSeller.Id)
                .Where(x => x.Status > 0)
                .Include(x => x.Products)
                    .ThenInclude(p => p.File)
                        .ThenInclude(f => f.AppSeller)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Package)
                        .ThenInclude(x => x.SheetsPerPage)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Package)
                        .ThenInclude(x => x.PaperSize)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Package)
                        .ThenInclude(x => x.PaperType)
                .Include(x=>x.AppCustomer)
                    .ThenInclude(x=>x.CustomerAddress)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new GetOrderDetailsForSeller
                {
                    OrderID = x.ID.ToString(),
                    CustomerName = x.AppCustomer.FirstName+" "+x.AppCustomer.LastName,
                    CustomerAddress = x.AppCustomer.CustomerAddress.Address,
                    CustomerPhoneNumber = x.AppCustomer.PhoneNumber,
                    CreatedDate = x.CreatedDate.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss"),
                    DeliveryDate = x.UpdatedDate.HasValue && x.Status == 4 ? x.UpdatedDate.Value.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss") : null,
                    OrderStatus = x.Status.ToString(),
                    Price = x.Price.ToString(),
                    OrderItems = x.Products.Select(product => new GetOrderDetailsItemForSeller
                    {
                        ProductNote = product.CustomerNote,
                        FileID = product.FileID.ToString(),
                        FileTitle = product.File.Title,
                        FileNote = product.File.Note,
                        FileNumberOfFile = product.File.NumberOfPage.ToString(),
                        Quantity = product.Quantity.ToString(),
                        Price = product.Price.ToString(),
                        PackagePrice = product.Package.Price.ToString(),
                        PackageColorMode = product.Package.ColorMode ? "Renkli" : "Siyah-Beyaz",
                        PackageDuplexMode = product.Package.DuplexMode ? "Arkalı-Önlü" : "Tek Yüz",
                        PackageSheetsPerPage = product.Package.SheetsPerPage.SheetsPerPageNumber.ToString(),
                        PackagePageSize = product.Package.PaperSize.SizeName,
                        PackagePaperType = product.Package.PaperType.PaperTypeName
                    }).ToList()
                }).ToListAsync();
                if (orderInfo != null)
                    return orderInfo;
                else
                    return new List<GetOrderDetailsForSeller>();
            }
            return new List<GetOrderDetailsForSeller>();
        }

        public async Task<IEnumerable<GetOrderDetailsForSeller>> GetOrderDetailsForSellerFilterAsync(VM_FilterGet_Order vmFilterGetOrder)
        {
            AppSeller? appSeller = await FindSeller();
            if (appSeller != null)
            {
                var orderInfo = await _orderReadRepository
                .Table
                .Where(x => x.AppSeller.Id == appSeller.Id)
                .Where(x => string.IsNullOrEmpty(vmFilterGetOrder.FilterCustomerName) || (x.AppCustomer.FirstName + x.AppCustomer.LastName).Contains(vmFilterGetOrder.FilterCustomerName))
                .Where(x => string.IsNullOrEmpty(vmFilterGetOrder.FilterCustomerPhoneNumber) || x.AppCustomer.PhoneNumber.Contains(vmFilterGetOrder.FilterCustomerPhoneNumber))
                .Where(x => vmFilterGetOrder.FilterProductStatus > 0 ? x.Status == vmFilterGetOrder.FilterProductStatus: x.Status>0)
                .Include(x => x.Products)
                    .ThenInclude(p => p.File)
                        .ThenInclude(f => f.AppSeller)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Package)
                        .ThenInclude(x => x.SheetsPerPage)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Package)
                        .ThenInclude(x => x.PaperSize)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Package)
                        .ThenInclude(x => x.PaperType)
                .Include(x => x.AppCustomer)
                    .ThenInclude(x => x.CustomerAddress)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new GetOrderDetailsForSeller
                {
                    OrderID = x.ID.ToString(),
                    CustomerName = x.AppCustomer.FirstName + " " + x.AppCustomer.LastName,
                    CustomerAddress = x.AppCustomer.CustomerAddress.Address,
                    CustomerPhoneNumber = x.AppCustomer.PhoneNumber,
                    CreatedDate = x.CreatedDate.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss"),
                    DeliveryDate = x.UpdatedDate.HasValue && x.Status == 4 ? x.UpdatedDate.Value.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss") : null,
                    OrderStatus = x.Status.ToString(),
                    Price = x.Price.ToString(),
                    OrderItems = x.Products.Select(product => new GetOrderDetailsItemForSeller
                    {
                        ProductNote = product.CustomerNote,
                        FileID = product.FileID.ToString(),
                        FileTitle = product.File.Title,
                        FileNote = product.File.Note,
                        FileNumberOfFile = product.File.NumberOfPage.ToString(),
                        Quantity = product.Quantity.ToString(),
                        Price = product.Price.ToString(),
                        PackagePrice = product.Package.Price.ToString(),
                        PackageColorMode = product.Package.ColorMode ? "Renkli" : "Siyah-Beyaz",
                        PackageDuplexMode = product.Package.DuplexMode ? "Arkalı-Önlü" : "Tek Yüz",
                        PackageSheetsPerPage = product.Package.SheetsPerPage.SheetsPerPageNumber.ToString(),
                        PackagePageSize = product.Package.PaperSize.SizeName,
                        PackagePaperType = product.Package.PaperType.PaperTypeName
                    }).ToList()
                }).ToListAsync();
                if (orderInfo != null)
                    return orderInfo;
                else
                    return new List<GetOrderDetailsForSeller>();
            }
            return new List<GetOrderDetailsForSeller>();
        }

        public async Task<UpdateOrderStatusDTO> UpdateOrderStatusAsync(VM_Update_Order_Status vmUpdateOrderStatus)
        {
            AppSeller? appSeller = await FindSeller();
            if(appSeller != null)
            {
                Order order = await _orderReadRepository.GetByIdAsync(vmUpdateOrderStatus.OrderID);
                order.Status = Convert.ToInt32(vmUpdateOrderStatus.Status);
                bool success = _orderWriteRepository.Update(order);
                await _orderWriteRepository.SaveAsync();
                if(success)
                    return new() { Message = "Sipariş durumu değiştirildi", Succeeded = true };
                else
                    return new() { Message = "Sipariş durumu değiştirilemedi", Succeeded = false };

            }
            return new() { Message = "Kullanıcı doğrulanamadı", Succeeded = false };
        }
    }
}
