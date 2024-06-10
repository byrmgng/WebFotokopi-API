using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.Abstraction.Tokens;
using WebFotokopi.Application.DTOs;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Infrastructure.Services.Tokens
{
    public class CustomerTokenHandler : ICustomerTokenHandler
    {
        readonly IConfiguration _configuration;

        public CustomerTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenDTO CreateAccessToken(AppCustomer appCustomer)
        {
            TokenDTO token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["CustomerToken:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            token.Expiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["CustomerToken:LifeTimeMinute"]));
            JwtSecurityToken securityToken = new JwtSecurityToken(
                audience: _configuration["CustomerToken:Audience"],
                issuer: _configuration["CustomerToken:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name, appCustomer.UserName) { } }
                );
            //token oluşturucu sınıfından bir örnek alınmalı
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
