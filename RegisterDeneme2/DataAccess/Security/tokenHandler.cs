using DataAccess.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Security
{
    public class tokenHandler : IJWT
    {
        private readonly IConfiguration _configuration;

        public tokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim ("UserId",user.Id)
            };
           

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));





            JwtSecurityToken jwtSecurityToken = new(
                claims: claims,
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: DateTime.Now.AddDays(Convert.ToInt16(2)),
     
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)) ;
           
            
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);




        }
    }
}
