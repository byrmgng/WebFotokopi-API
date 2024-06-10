using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;
using System.Text;
using WebFotokopi.API.Configurations;
using WebFotokopi.API.Middlewares;
using WebFotokopi.Application;
using WebFotokopi.Infrastructure;
using WebFotokopi.Persistence;
namespace WebFotokopi.API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddPersistenceServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Standart", builder =>
                    builder.WithOrigins("http://localhost:4200", "https://localhost:4200", "http://localhost:4201", "https://localhost:4201")
                           .AllowAnyHeader()
                           .AllowAnyMethod());
            });
            
            builder.Host.UseSerilog(LoggerConfigurationFactory.CreateLogger(builder.Configuration));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddAuthentication().AddJwtBearer("Seller", options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true, //Olu�turulacak Tokenin hangi sitelerde kullan�laca�� belirlenir.
                    ValidateIssuer = true, // Olu�turulacak token de�erini kim olu�turulacak belirlenir.
                    ValidateLifetime = true, // Olu�turulan token de�erinin s�resini kontrol etmek i�in kullan�l�r.
                    ValidateIssuerSigningKey = true, //�retilecek token de�erinin uygulamaya ait olma durmunu kontrol eder.
                    ValidAudience = builder.Configuration["SellerToken:Audience"],
                    ValidIssuer = builder.Configuration["SellerToken:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SellerToken:SecurityKey"])),
                    LifetimeValidator = (notbefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                    NameClaimType = ClaimTypes.Name
                };
            }).AddJwtBearer("Customer",options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true, //Olu�turulacak Tokenin hangi sitelerde kullan�laca�� belirlenir.
                    ValidateIssuer = true, // Olu�turulacak token de�erini kim olu�turulacak belirlenir.
                    ValidateLifetime = true, // Olu�turulan token de�erinin s�resini kontrol etmek i�in kullan�l�r.
                    ValidateIssuerSigningKey = true, //�retilecek token de�erinin uygulamaya ait olma durmunu kontrol eder.
                    ValidAudience = builder.Configuration["CustomerToken:Audience"],
                    ValidIssuer = builder.Configuration["CustomerToken:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["CustomerToken:SecurityKey"])),
                    LifetimeValidator = (notbefore,expires,securityToken,validationParameters) => expires!=null ? expires > DateTime.UtcNow:false,
                    NameClaimType = ClaimTypes.Name
                };
            });

            

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseStaticFiles();
            app.UseCors("Standart"); 
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<UsernameMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
