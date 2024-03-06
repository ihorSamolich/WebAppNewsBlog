﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAppNewsBlog.Data.Entities.Identity;
using WebAppNewsBlog.Interfaces;

namespace WebAppNewsBlog.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(UserEntity user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new ("email", user.Email),
                new ("name", $"{user.LastName} {user.FirstName}"),
                new ("image", user.Image),
            };

            claims.AddRange(roles.Select(role => new Claim("roles", role)));

            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSecretKey"));
            var signinKey = new SymmetricSecurityKey(key);
            var signinCredential = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredential,
                expires: DateTime.Now.AddDays(10),
                //expires: DateTime.Now.AddMinutes(1),
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
