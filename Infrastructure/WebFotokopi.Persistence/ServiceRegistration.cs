using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.Repositories.CityRepositories;
using WebFotokopi.Application.Repositories.CustomerAddressRepositories;
using WebFotokopi.Application.Repositories.DistrictRepositories;
using WebFotokopi.Application.Repositories.FileRepositories;
using WebFotokopi.Application.Repositories.OrderRepositories;
using WebFotokopi.Application.Repositories.ProductFeatureRepositories;
using WebFotokopi.Application.Repositories.ProductRepositories;
using WebFotokopi.Application.Repositories.SellerAddressRepositories;
using WebFotokopi.Application.Repositories.SellerPaperSizeRepositories;
using WebFotokopi.Application.Repositories.SellerPaperTypeRepositories;
using WebFotokopi.Application.Repositories.SellerSheetsPerPageRepositories;
using WebFotokopi.Domain.Entities.Identity;
using WebFotokopi.Persistence.Contexts;
using WebFotokopi.Persistence.Repositories.CityRepositories;
using WebFotokopi.Persistence.Repositories.CustomerAddressRepositories;
using WebFotokopi.Persistence.Repositories.DistrictRepositories;
using WebFotokopi.Persistence.Repositories.FileRepositories;
using WebFotokopi.Persistence.Repositories.OrderRepositories;
using WebFotokopi.Persistence.Repositories.ProductFeatureRepositories;
using WebFotokopi.Persistence.Repositories.ProductRepositories;
using WebFotokopi.Persistence.Repositories.SellerAddressRepotories;
using WebFotokopi.Persistence.Repositories.SellerPaperSizeRepositories;
using WebFotokopi.Persistence.Repositories.SellerPaperTypeRepositories;
using WebFotokopi.Persistence.Repositories.SellerSheetsPerPageRepositories;
using WebFotokopi.Persistence.Services;

namespace WebFotokopi.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<WebFotokopiDbContext>(options=>options.UseSqlServer(Configurations.ConnectionString)); 

            services.AddScoped<ICityReadRepository, CityReadRepository>();
            services.AddScoped<ICityWriteRepository, CityWriteRepository>();
            services.AddScoped<IDistrictReadRepository, DistrictReadRepository>();
            services.AddScoped<IDistrictWriteRepository, DistrictWriteRepository>();
            services.AddScoped<ISellerAddressReadRepository, AddressReadRepository>();
            services.AddScoped<ISellerAddressWriteRepository, AddressWriteRepository>();
            services.AddScoped<ISheetsPerPageReadReposity, SheetsPerPageReadRepository>();
            services.AddScoped<ISheetsPerPageWriteReposity, SheetsPerPageWriteRepository>();
            services.AddScoped<IPaperSizeReadRepository, PaperSizeReadRepository>();
            services.AddScoped<IPaperSizeWriteRepository, PaperSizeWriteRepository>();
            services.AddScoped<IPaperTypeReadRepository, PaperTypeReadRepository>();
            services.AddScoped<IPaperSizeWriteRepository, PaperSizeWriteRepository>();
            services.AddScoped<IPackageReadReposity, PackageReadRepository>();
            services.AddScoped<IPackageWriteRepository, PackageWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<ICustomerAddressReadRepository, CustomerAddressReadRepository>();
            services.AddScoped<ICustomerAddressWriteRepository, CustomerAddressWriteRepository>();
            services.AddScoped<IFileReadRepository,FileReadRepository>();
            services.AddScoped<IFileWriteRepository,FileWriteRepository>();

            services.AddScoped<ICityService,CityService>();
            services.AddScoped<IDistrictService,DistrictService>();
            services.AddScoped<ISellerService, SellerService>();
            services.AddScoped<ISellerAddressService, SellerAddressService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerAddressService, CustomerAddressService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService,OrderService>();

            services.AddIdentityCore<AppSeller>()
                .AddEntityFrameworkStores<WebFotokopiDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<AppSeller>>();

            services.AddIdentityCore<AppCustomer>()
                .AddEntityFrameworkStores<WebFotokopiDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<AppCustomer>>();
        }
    }
}
