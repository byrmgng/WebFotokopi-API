using Microsoft.Extensions.DependencyInjection;
using WebFotokopi.Application.Abstraction.Tokens;
using WebFotokopi.Infrastructure.Services.Tokens;

namespace WebFotokopi.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection collection)
        {
            collection.AddScoped<ICustomerTokenHandler,CustomerTokenHandler>();
            collection.AddScoped<ISellerTokenHandler,SellerTokenHandler>();
        }
    }
}
