using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LearnApi.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LearnApi.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<Config> _config;

        public TokenService(IOptions<Config> config)
        {
            this._config = config;
        }

        public object GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_config.Value.JwtSecret);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]{
                    new Claim("userId", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }
    }
}