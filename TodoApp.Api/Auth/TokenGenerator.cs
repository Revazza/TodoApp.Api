using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using TodoApp.Api.Db.Entities;

namespace TodoApp.Api.Auth
{
    public class TokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(List<Claim> claims)
        {
            var issuer = _configuration["JWT:Issuer"]!;
            var audience = _configuration["JWT:Audience"]!;
            var secretKey = _configuration["JWT:SecretKey"]!;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            var tokenGenerator = new JwtSecurityTokenHandler();
            var jwtToken = tokenGenerator.WriteToken(token);

            return jwtToken;
        }
    }
}
