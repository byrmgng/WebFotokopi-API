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
    public class SellerTokenHandler : ISellerTokenHandler
    {
        readonly IConfiguration _configuration;

        public SellerTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDTO CreateAccessToken(AppSeller appSeller)
        {
            TokenDTO token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["SellerToken:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);


            token.Expiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["SellerToken:LifeTimeMinute"]));
            JwtSecurityToken securityToken = new JwtSecurityToken(
                audience: _configuration["SellerToken:Audience"],
                issuer: _configuration["SellerToken:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name, appSeller.UserName) { } }
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
