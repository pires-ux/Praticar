using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

namespace Api01
{
    public class JWTservice
    {

        private readonly IConfiguration _configuration;
        public JWTservice(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int id)
        {
            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };
             
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claim,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
