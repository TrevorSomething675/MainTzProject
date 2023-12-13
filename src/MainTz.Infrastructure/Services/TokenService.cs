using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using MainTz.Application.Services;
using MainTz.Extensions.Models;
using System.Security.Claims;
using MainTz.Extensions;
using System.Text;

namespace MainTz.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtAuthSettings _authSettings;
        public TokenService()
        {
            _authSettings = Settings.Load<JwtAuthSettings>("AuthSettings");
        }

        public string CreateAccessToken(string role)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Role, role) };
            var jwt = new JwtSecurityToken(
                    issuer: _authSettings.Issuer,
                    audience: _authSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(8)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_authSettings.Key)),
                        SecurityAlgorithms.HmacSha256)
                    );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string CreateRefreshToken(string role)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Role, role) };
            var jwt = new JwtSecurityToken(
                    issuer: _authSettings.Issuer,
                    audience: _authSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(3)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_authSettings.Key)),
                        SecurityAlgorithms.HmacSha256)
                    );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
